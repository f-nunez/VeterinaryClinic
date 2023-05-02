using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Clinics;

public partial class ClinicsComponent : ComponentBase
{
    [Inject]
    private IClinicService _clinicService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    [Inject]
    private IStringLocalizer<ClinicDetailComponent> _stringLocalizerForDetail { get; set; }

    [Inject]
    private IStringLocalizer<ClinicsFilterComponent> _stringLocalizerForFilter { get; set; }

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

    protected async Task OnClickDetail(ClinicDto doctor)
    {
        _spinnerService.Show();

        var request = new GetClinicByIdRequest
        {
            Id = doctor.Id
        };

        var currentClinic = await _clinicService.GetByIdAsync(request);

        var clinicForDetail = ClinicHelper.MapClinicViewModel(currentClinic);

        _spinnerService.Hide();

        await _dialogService.OpenAsync<ClinicDetail>(
            _stringLocalizerForDetail["ClinicDetail_Label_ClinicDetail"],
            new Dictionary<string, object>
            {
                { "Model", clinicForDetail }
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
}