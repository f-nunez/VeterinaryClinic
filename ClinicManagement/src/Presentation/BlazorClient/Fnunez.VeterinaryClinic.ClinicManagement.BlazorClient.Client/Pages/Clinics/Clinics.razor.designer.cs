using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Clinics;

public partial class ClinicsComponent : ComponentBase
{
    [Inject]
    private IClinicService _ClinicService { get; set; }

    protected RadzenDataGrid<ClinicDto> ClinicsGrid;

    protected List<ClinicDto> Clinics;

    protected int Count;

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected IStringLocalizer<ClinicsComponent> StringLocalizer { get; set; }

    [Inject]
    protected IStringLocalizer<ClinicsFilterComponent> StringLocalizerForFilter { get; set; }

    protected bool IsLoading = false;

    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };

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

        var result = await DialogService.OpenSideAsync<ClinicsFilter>(
            StringLocalizerForFilter["ClinicsFilter_Label_Filter"],
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