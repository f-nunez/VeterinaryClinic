using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Rooms;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Doctors;

public partial class DoctorsComponent : ComponentBase
{
    [Inject]
    private IDoctorService _doctorService { get; set; }

    protected RadzenDataGrid<DoctorDto> DoctorsGrid;

    protected List<DoctorDto> Doctors;

    protected int Count;

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected IStringLocalizer<DoctorsComponent> StringLocalizer { get; set; }

    [Inject]
    protected IStringLocalizer<DoctorsFilterComponent> StringLocalizerForFilter { get; set; }

    protected bool IsLoading = false;

    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };

    protected string FullNameFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    protected string SearchFilterValue { get; set; }

    protected async Task LoadData(LoadDataArgs args)
    {
        IsLoading = true;
        var request = new GetDoctorsRequest
        {
            DataGridRequest = args.GetDataGridRequest(),
            FullNameFilterValue = FullNameFilterValue,
            IdFilterValue = IdFilterValue,
            SearchFilterValue = SearchFilterValue
        };

        var dataGridResponse = await _doctorService
            .DataGridAsync(request);

        Count = dataGridResponse.Count;
        Doctors = dataGridResponse.Items;
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
        var filterValues = new DoctorsFilterValues
        {
            FullNameFilterValue = FullNameFilterValue,
            IdFilterValue = IdFilterValue
        };

        var filterParameters = new Dictionary<string, object>()
        {
            { nameof(DoctorsFilterValues), filterValues }
        };

        var result = await DialogService.OpenSideAsync<DoctorsFilter>(
            StringLocalizerForFilter["DoctorsFilter_Label_Filter"],
            filterParameters
        );

        await ProcessClosedFilterMenuAsync(result);
    }

    private async Task ProcessClosedFilterMenuAsync(DoctorsFilterValues filterValues)
    {
        if (filterValues is null)
            return;

        FullNameFilterValue = filterValues.FullNameFilterValue;
        IdFilterValue = filterValues.IdFilterValue;

        await ResetGridAndSearchAsync();
    }

    private async Task ResetGridAndSearchAsync()
    {
        DoctorsGrid.Reset(false);
        await DoctorsGrid.FirstPage(true);
    }
}