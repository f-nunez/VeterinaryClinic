using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.DeleteClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Clinics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Clinics;

public partial class ClinicsComponent : ComponentBase
{
    [Inject]
    private IClinicService _clinicService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    [Inject]
    private IStringLocalizer<AddEditClinicComponent> _stringLocalizerForAdd { get; set; }

    [Inject]
    private IStringLocalizer<ClinicDetailComponent> _stringLocalizerForDetail { get; set; }

    [Inject]
    private IStringLocalizer<ClinicsFilterComponent> _stringLocalizerForFilter { get; set; }

    protected bool CanWrite { get; set; }

    protected RadzenDataGrid<ClinicDto> ClinicsGrid;

    protected List<ClinicDto> Clinics;

    protected int Count;

    [Inject]
    protected IStringLocalizer<ClinicsComponent> StringLocalizer { get; set; }

    protected bool IsLoading = false;

    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };

    protected string AddressFilterValue { get; set; }

    protected string EmailAddressFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    protected string NameFilterValue { get; set; }

    protected string SearchFilterValue { get; set; }

    protected async Task LoadData(LoadDataArgs args)
    {
        _spinnerService.Show();
        IsLoading = true;
        var request = new GetClinicsRequest
        {
            DataGridRequest = args.GetDataGridRequest(),
            AddressFilterValue = AddressFilterValue,
            EmailAddressFilterValue = EmailAddressFilterValue,
            IdFilterValue = IdFilterValue,
            NameFilterValue = NameFilterValue,
            SearchFilterValue = SearchFilterValue
        };

        var dataGridResponse = await _clinicService
            .DataGridAsync(request);

        Clinics = dataGridResponse.Items;
        Count = dataGridResponse.Count;
        IsLoading = false;
        _spinnerService.Hide();

        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnClickAdd()
    {
        var response = await _dialogService.OpenAsync<AddEditClinic>(
            _stringLocalizerForAdd["AddEditClinic_Label_Add"],
            new Dictionary<string, object>
            {
                { "IsClinicToAdd", true }
            }
        );

        if (response is null)
            return;

        var savedClinic = response as ClinicVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Clinics_AddedClinic_Alert_Message"], savedClinic.Name),
            StringLocalizer["Clinics_AddedClinic_Alert_Title"],
            StringLocalizer["Clinics_AddedClinic_Alert_Button_Ok"]);

        await ClinicsGrid.Reload();
    }

    protected async Task OnClickDelete(ClinicDto clinic)
    {
        string message = string.Format(
            StringLocalizer["Clinics_DeleteClinic_Alert_Message"],
            clinic.Name);

        bool? proceedToDelete = await _dialogService.Confirm(
            message,
            StringLocalizer["Clinics_DeleteClinic_Alert_Title"],
            new ConfirmOptions
            {
                OkButtonText = StringLocalizer["Clinics_DeleteClinic_Alert_Button_Ok"],
                CancelButtonText = StringLocalizer["Clinics_DeleteClinic_Alert_Button_Cancel"]
            }
        );

        if (!proceedToDelete.HasValue || !proceedToDelete.Value)
            return;

        _spinnerService.Show();

        var request = new DeleteClinicRequest
        {
            Id = clinic.Id
        };

        await _clinicService.DeleteAsync(request);

        _spinnerService.Hide();

        await ShowAlertAsync(
            string.Format(StringLocalizer["Clinics_DeletedClinic_Alert_Message"], clinic.Name),
            StringLocalizer["Clinics_DeletedClinic_Alert_Title"],
            StringLocalizer["Clinics_DeletedClinic_Alert_Button_Ok"]);

        await ClinicsGrid.ReloadAfterDeleteItemAsync();
    }

    protected async Task OnClickDetail(ClinicDto clinic)
    {
        _spinnerService.Show();

        var request = new GetClinicByIdRequest
        {
            Id = clinic.Id
        };

        var currentClinic = await _clinicService.GetByIdAsync(request);

        var clinicForDetail = ClinicHelper.MapClinicViewModel(clinic);

        _spinnerService.Hide();

        await _dialogService.OpenAsync<ClinicDetail>(
            _stringLocalizerForDetail["ClinicDetail_Label_ClinicDetail"],
            new Dictionary<string, object>
            {
                { "Model", clinicForDetail }
            }
        );
    }

    protected async Task OnClickEdit(ClinicDto clinic)
    {
        _spinnerService.Show();

        var request = new GetClinicByIdRequest
        {
            Id = clinic.Id
        };

        var currentClinic = await _clinicService.GetByIdAsync(request);

        var clinicToEdit = ClinicHelper.MapClinicViewModel(clinic);

        _spinnerService.Hide();

        var response = await _dialogService.OpenAsync<AddEditClinic>(
            _stringLocalizerForAdd["AddEditClinic_Label_Edit"],
            new Dictionary<string, object>
            {
                { "IsClinicToAdd", false },
                { "Model", clinicToEdit }
            }
        );

        if (response is null)
            return;

        var savedClinic = response as ClinicVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Clinics_EditedClinic_Alert_Message"], savedClinic.Name),
            StringLocalizer["Clinics_EditedClinic_Alert_Title"],
            StringLocalizer["Clinics_EditedClinic_Alert_Button_Ok"]);

        await ClinicsGrid.Reload();
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
        var filterValues = new ClinicsFilterValues
        {
            AddressFilterValue = AddressFilterValue,
            EmailAddressFilterValue = EmailAddressFilterValue,
            IdFilterValue = IdFilterValue,
            NameFilterValue = NameFilterValue
        };

        var filterParameters = new Dictionary<string, object>()
        {
            { nameof(ClinicsFilterValues), filterValues }
        };

        var result = await _dialogService.OpenSideAsync<ClinicsFilter>(
            _stringLocalizerForFilter["ClinicsFilter_Label_Filter"],
            filterParameters
        );

        await ProcessClosedFilterMenuAsync(result);
    }

    private async Task ProcessClosedFilterMenuAsync(ClinicsFilterValues filterValues)
    {
        if (filterValues is null)
            return;

        AddressFilterValue = filterValues.AddressFilterValue;
        EmailAddressFilterValue = filterValues.EmailAddressFilterValue;
        IdFilterValue = filterValues.IdFilterValue;
        NameFilterValue = filterValues.NameFilterValue;

        await ResetGridAndSearchAsync();
    }

    private async Task ResetGridAndSearchAsync()
    {
        ClinicsGrid.Reset(false);
        await ClinicsGrid.FirstPage(true);
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