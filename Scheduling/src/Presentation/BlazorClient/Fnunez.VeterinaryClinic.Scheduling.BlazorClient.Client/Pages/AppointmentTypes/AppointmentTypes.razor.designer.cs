using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Extensions;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.AppointmentTypes;

public partial class AppointmentTypesComponent : ComponentBase
{
    [Inject]
    private AppointmentTypeService _appointmentTypeService { get; set; }
    protected RadzenDataGrid<AppointmentTypeDto> AppointmentTypesGrid;
    protected List<AppointmentTypeDto> AppointmentTypes;
    protected int Count;
    [Inject]
    protected DialogService DialogService { get; set; }
    protected bool IsLoading = false;
    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };
    protected string PagingSummaryFormat = "Displaying page {0} of {1} (total {2} records)";

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

        var result = await DialogService
            .OpenSideAsync<AppointmentTypesFilter>(
                "Filter Menu",
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