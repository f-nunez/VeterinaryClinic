using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.AppointmentTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.AppointmentTypes;

public partial class AppointmentTypesComponent : ComponentBase
{
    [Inject]
    private IAppointmentTypeService _appointmentTypeService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IStringLocalizer<AddEditAppointmentTypeComponent> _stringLocalizerForAdd { get; set; }

    [Inject]
    private IStringLocalizer<AppointmentTypeDetailComponent> _stringLocalizerForDetail { get; set; }

    [Inject]
    private IStringLocalizer<AppointmentTypesFilterComponent> _stringLocalizerForFilter { get; set; }

    protected RadzenDataGrid<AppointmentTypeDto> AppointmentTypesGrid;

    protected List<AppointmentTypeDto> AppointmentTypes;

    protected bool CanWrite { get; set; }

    protected int Count;

    [Inject]
    protected IStringLocalizer<AppointmentTypesComponent> StringLocalizer { get; set; }

    protected bool IsLoading = false;

    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };

    protected string CodeFilterValue { get; set; }

    protected string DurationFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    protected string NameFilterValue { get; set; }

    protected string SearchFilterValue { get; set; }

    protected async Task LoadData(LoadDataArgs args)
    {
        IsLoading = true;
        var request = new GetAppointmentTypesRequest
        {
            CodeFilterValue = CodeFilterValue,
            DataGridRequest = args.GetDataGridRequest(),
            DurationFilterValue = DurationFilterValue,
            IdFilterValue = IdFilterValue,
            NameFilterValue = NameFilterValue,
            SearchFilterValue = SearchFilterValue
        };

        var dataGridResponse = await _appointmentTypeService
            .DataGridAsync(request);

        AppointmentTypes = dataGridResponse.Items;
        Count = dataGridResponse.Count;
        IsLoading = false;

        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnClickAdd()
    {
        var response = await _dialogService.OpenAsync<AddEditAppointmentType>(
            _stringLocalizerForAdd["AddEditAppointmentType_Label_Add"],
            new Dictionary<string, object>
            {
                { "IsAppointmentTypeToAdd", true }
            }
        );

        if (response is null)
            return;

        var savedAppointmentType = response as AppointmentTypeVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["AppointmentTypes_AddedAppointmentType_Alert_Message"], savedAppointmentType.Name),
            StringLocalizer["AppointmentTypes_AddedAppointmentType_Alert_Title"],
            StringLocalizer["AppointmentTypes_AddedAppointmentType_Alert_Button_Ok"]);

        await AppointmentTypesGrid.Reload();
    }

    protected async Task OnClickDelete(AppointmentTypeDto appointmentType)
    {
        string message = string.Format(
            StringLocalizer["AppointmentTypes_DeleteAppointmentType_Alert_Message"],
            appointmentType.Name);

        bool? proceedToDelete = await _dialogService.Confirm(
            message,
            StringLocalizer["AppointmentTypes_DeleteAppointmentType_Alert_Title"],
            new ConfirmOptions
            {
                OkButtonText = StringLocalizer["AppointmentTypes_DeleteAppointmentType_Alert_Button_Ok"],
                CancelButtonText = StringLocalizer["AppointmentTypes_DeleteAppointmentType_Alert_Button_Cancel"]
            }
        );

        if (!proceedToDelete.HasValue || !proceedToDelete.Value)
            return;

        var request = new DeleteAppointmentTypeRequest
        {
            Id = appointmentType.Id
        };

        await _appointmentTypeService.DeleteAsync(request);

        await ShowAlertAsync(
            string.Format(StringLocalizer["AppointmentTypes_DeletedAppointmentType_Alert_Message"], appointmentType.Name),
            StringLocalizer["AppointmentTypes_DeletedAppointmentType_Alert_Title"],
            StringLocalizer["AppointmentTypes_DeletedAppointmentType_Alert_Button_Ok"]);

        await AppointmentTypesGrid.Reload();
    }

    protected async Task OnClickDetail(AppointmentTypeDto appointmentType)
    {
        var request = new GetAppointmentTypeByIdRequest
        {
            Id = appointmentType.Id
        };

        var currentAppointmentType = await _appointmentTypeService
            .GetByIdAsync(request);

        var appointmentTypeForDetail = AppointmentTypeHelper
            .MapAppointmentTypeViewModel(appointmentType);

        await _dialogService.OpenAsync<AppointmentTypeDetail>(
            _stringLocalizerForDetail["AppointmentTypeDetail_Label_AppointmentTypeDetail"],
            new Dictionary<string, object>
            {
                { "Model", appointmentTypeForDetail }
            }
        );
    }

    protected async Task OnClickEdit(AppointmentTypeDto appointmentType)
    {
        var request = new GetAppointmentTypeByIdRequest
        {
            Id = appointmentType.Id
        };

        var currentAppointmentType = await _appointmentTypeService
            .GetByIdAsync(request);

        var appointmentTypeToEdit = AppointmentTypeHelper
            .MapAppointmentTypeViewModel(appointmentType);

        var response = await _dialogService.OpenAsync<AddEditAppointmentType>(
            _stringLocalizerForAdd["AddEditAppointmentType_Label_Edit"],
            new Dictionary<string, object>
            {
                { "IsAppointmentTypeToAdd", false },
                { "Model", appointmentTypeToEdit }
            }
        );

        if (response is null)
            return;

        var savedAppointmentType = response as AppointmentTypeVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["AppointmentTypes_EditedAppointmentType_Alert_Message"], savedAppointmentType.Name),
            StringLocalizer["AppointmentTypes_EditedAppointmentType_Alert_Title"],
            StringLocalizer["AppointmentTypes_EditedAppointmentType_Alert_Button_Ok"]);

        await AppointmentTypesGrid.Reload();
    }

    protected async Task OnClickFilterSearch()
    {
        await ResetGridAndSearchAsync();
    }

    protected async Task OnKeyUpFilterSearch(KeyboardEventArgs args)
    {
        if (args.Key != "Enter")
            return;

        await ResetGridAndSearchAsync();
    }

    protected async Task OpenFilterMenu()
    {
        var filterValues = new AppointmentTypesFilterValues
        {
            CodeFilterValue = CodeFilterValue,
            DurationFilterValue = DurationFilterValue,
            IdFilterValue = IdFilterValue,
            NameFilterValue = NameFilterValue
        };

        var filterParameters = new Dictionary<string, object>()
        {
            { nameof(AppointmentTypesFilterValues), filterValues }
        };

        var result = await _dialogService.OpenSideAsync<AppointmentTypesFilter>(
            _stringLocalizerForFilter["AppointmentTypesFilter_Label_Filter"],
            filterParameters
        );

        await ProcessClosedFilterMenuAsync(result);
    }

    private async Task ProcessClosedFilterMenuAsync(AppointmentTypesFilterValues filterValues)
    {
        if (filterValues is null)
            return;

        CodeFilterValue = filterValues.CodeFilterValue;
        DurationFilterValue = filterValues.DurationFilterValue;
        IdFilterValue = filterValues.IdFilterValue;
        NameFilterValue = filterValues.NameFilterValue;

        await ResetGridAndSearchAsync();
    }

    private async Task ResetGridAndSearchAsync()
    {
        AppointmentTypesGrid.Reset(false);
        await AppointmentTypesGrid.FirstPage(true);
    }

    private async Task<bool?> ShowAlertAsync(
        string alertMessage,
        string alertTitle,
        string alertButtonOkMessage)
    {
        return await _dialogService.Alert(
            alertMessage,
            alertTitle,
            new AlertOptions
            {
                OkButtonText = alertButtonOkMessage
            }
        );
    }
}