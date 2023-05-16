using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.AppointmentTypes;

public partial class AppointmentTypesComponent : ComponentBase
{
    [Inject]
    private IAppointmentTypeService _appointmentTypeService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    [Inject]
    private IStringLocalizer<AppointmentTypeDetailComponent> _stringLocalizerForDetail { get; set; }

    [Inject]
    private IStringLocalizer<AppointmentTypesFilterComponent> _stringLocalizerForFilter { get; set; }

    protected RadzenDataGrid<AppointmentTypeDto> AppointmentTypesGrid;

    protected List<AppointmentTypeDto> AppointmentTypes;

    protected int Count;

    [Inject]
    protected IStringLocalizer<AppointmentTypesComponent> StringLocalizer { get; set; }

    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };

    protected string CodeFilterValue { get; set; }

    protected string DurationFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    protected string NameFilterValue { get; set; }

    protected string SearchFilterValue { get; set; }

    protected async Task LoadData(LoadDataArgs args)
    {
        _spinnerService.Show();
        var request = new GetAppointmentTypesRequest
        {
            CodeFilterValue = CodeFilterValue,
            DataGridRequest = args.GetDataGridRequest(),
            DurationFilterValue = DurationFilterValue,
            IdFilterValue = IdFilterValue,
            NameFilterValue = NameFilterValue
        };

        request.DataGridRequest.Search = SearchFilterValue;

        var dataGridResponse = await _appointmentTypeService
            .DataGridAsync(request);

        AppointmentTypes = dataGridResponse.Items;
        Count = dataGridResponse.Count;
        _spinnerService.Hide();

        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnClickDetail(AppointmentTypeDto appointmentType)
    {
        _spinnerService.Show();

        var request = new GetAppointmentTypeByIdRequest
        {
            Id = appointmentType.Id
        };

        var currentAppointmentType = await _appointmentTypeService
            .GetByIdAsync(request);

        var appointmentTypeForDetail = AppointmentTypeHelper
            .MapAppointmentTypeViewModel(currentAppointmentType);

        _spinnerService.Hide();

        await _dialogService.OpenAsync<AppointmentTypeDetail>(
            _stringLocalizerForDetail["AppointmentTypeDetail_Label_AppointmentTypeDetail"],
            new Dictionary<string, object>
            {
                { "Model", appointmentTypeForDetail }
            }
        );
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
}