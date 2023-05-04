using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.AppointmentTypes;

public partial class AppointmentTypesFilterComponent : ComponentBase
{
    [Inject]
    private IAppointmentTypeService _appointmentTypeService { get; set; }

    protected IEnumerable<string> AppointmentTypeCodes;

    protected IEnumerable<string> AppointmentTypeDurations;

    protected IEnumerable<string> AppointmentTypeIds;

    protected IEnumerable<string> AppointmentTypeNames;

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected IStringLocalizer<AppointmentTypesFilterComponent> StringLocalizer { get; set; }

    protected string CodeFilterValue { get; set; }

    protected string DurationFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    protected string NameFilterValue { get; set; }

    [Parameter]
    public AppointmentTypesFilterValues AppointmentTypesFilterValues { get; set; }

    protected override void OnInitialized()
    {
        CodeFilterValue = AppointmentTypesFilterValues.CodeFilterValue;
        DurationFilterValue = AppointmentTypesFilterValues.DurationFilterValue;
        IdFilterValue = AppointmentTypesFilterValues.IdFilterValue;
        NameFilterValue = AppointmentTypesFilterValues.NameFilterValue;
    }

    protected void OnClickButtonFilter()
    {
        var filterValues = new AppointmentTypesFilterValues
        {
            CodeFilterValue = CodeFilterValue,
            DurationFilterValue = DurationFilterValue,
            IdFilterValue = IdFilterValue,
            NameFilterValue = NameFilterValue
        };

        DialogService.CloseSide(filterValues);
    }

    protected void OnClickButtonClean()
    {
        var filterValues = new AppointmentTypesFilterValues
        {
            CodeFilterValue = string.Empty,
            DurationFilterValue = string.Empty,
            IdFilterValue = string.Empty,
            NameFilterValue = string.Empty
        };

        DialogService.CloseSide(filterValues);
    }

    protected async void OnChangeCode(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            AppointmentTypeCodes = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async void OnChangeDuration(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            AppointmentTypeDurations = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async void OnChangeId(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            AppointmentTypeIds = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async void OnChangeName(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            AppointmentTypeNames = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async Task LoadDataAppointmentTypesFilterCode(LoadDataArgs args)
    {
        string filterValue = args.Filter;

        AppointmentTypeCodes = await _appointmentTypeService
            .DataGridFilterCodeAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }

    protected async Task LoadDataAppointmentTypesFilterDuration(LoadDataArgs args)
    {
        string filterValue = args.Filter;
        int integerValue = default;

        if (!int.TryParse(args.Filter, out integerValue) ||
            integerValue < 0)
        {
            AppointmentTypeDurations = null;
            await InvokeAsync(StateHasChanged);
            return;
        }

        AppointmentTypeDurations = await _appointmentTypeService
            .DataGridFilterDurationAsync(args.Filter);

        await InvokeAsync(StateHasChanged);
    }

    protected async Task LoadDataAppointmentTypesFilterId(LoadDataArgs args)
    {
        string filterValue = args.Filter;
        int integerValue = default;

        if (!int.TryParse(args.Filter, out integerValue) ||
            integerValue < 0)
        {
            AppointmentTypeIds = null;
            await InvokeAsync(StateHasChanged);
            return;
        }

        AppointmentTypeIds = await _appointmentTypeService
            .DataGridFilterIdAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }

    protected async Task LoadDataAppointmentTypesFilterName(LoadDataArgs args)
    {
        string filterValue = args.Filter;

        AppointmentTypeNames = await _appointmentTypeService
            .DataGridFilterNameAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }
}