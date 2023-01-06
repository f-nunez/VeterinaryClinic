using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Clinics;

public partial class ClinicsComponent : ComponentBase
{
    [Inject]
    private ClinicService _ClinicService { get; set; }
    protected RadzenDataGrid<ClinicDto> ClinicsGrid;
    protected List<ClinicDto> Clinics;
    protected int Count;
    [Inject]
    protected DialogService DialogService { get; set; }
    protected bool IsLoading = false;
    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };
    protected string PagingSummaryFormat = "Displaying page {0} of {1} (total {2} records)";

    protected string AddressFilterValue { get; set; }
    protected string EmailAddressFilterValue { get; set; }
    protected string IdFilterValue { get; set; }
    protected string NameFilterValue { get; set; }
    protected string SearchFilterValue { get; set; }

    protected async Task LoadData(LoadDataArgs args)
    {
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

        var dataGridResponse = await _ClinicService
            .DataGridAsync(request);

        Clinics = dataGridResponse.Items;
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

        var result = await DialogService
            .OpenSideAsync<ClinicsFilter>(
                "Filter Menu",
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
}