using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Clinics;

public partial class ClinicsFilterComponent : ComponentBase
{
    [Inject]
    private IClinicService _clinicService { get; set; }

    protected IEnumerable<string> ClinicAddresses;

    protected IEnumerable<string> ClinicEmailAddresses;

    protected IEnumerable<string> ClinicIds;

    protected IEnumerable<string> ClinicNames;

    [Inject]
    protected DialogService DialogService { get; set; }

    protected string AddressFilterValue { get; set; }

    protected string EmailAddressFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    protected string NameFilterValue { get; set; }

    [Parameter]
    public ClinicsFilterValues ClinicsFilterValues { get; set; }

    protected override void OnInitialized()
    {
        AddressFilterValue = ClinicsFilterValues.AddressFilterValue;
        EmailAddressFilterValue = ClinicsFilterValues.EmailAddressFilterValue;
        IdFilterValue = ClinicsFilterValues.IdFilterValue;
        NameFilterValue = ClinicsFilterValues.NameFilterValue;
    }

    protected void OnClickButtonFilter()
    {
        var filterValues = new ClinicsFilterValues
        {
            AddressFilterValue = AddressFilterValue,
            EmailAddressFilterValue = EmailAddressFilterValue,
            IdFilterValue = IdFilterValue,
            NameFilterValue = NameFilterValue
        };

        DialogService.CloseSide(filterValues);
    }

    protected void OnClickButtonClean()
    {
        var filterValues = new ClinicsFilterValues
        {
            AddressFilterValue = string.Empty,
            EmailAddressFilterValue = string.Empty,
            IdFilterValue = string.Empty,
            NameFilterValue = string.Empty
        };

        DialogService.CloseSide(filterValues);
    }

    protected async void OnChangeAddress(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            ClinicAddresses = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async void OnChangeEmailAddress(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            ClinicEmailAddresses = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async void OnChangeId(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            ClinicIds = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async void OnChangeName(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            ClinicNames = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async Task LoadDataClinicsFilterAddress(LoadDataArgs args)
    {
        string filterValue = args.Filter;

        ClinicAddresses = await _clinicService
            .DataGridFilterAddressAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }

    protected async Task LoadDataClinicsFilterEmailAddress(LoadDataArgs args)
    {
        string filterValue = args.Filter;

        ClinicEmailAddresses = await _clinicService
            .DataGridFilterEmailAddressAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }

    protected async Task LoadDataClinicsFilterId(LoadDataArgs args)
    {
        string filterValue = args.Filter;
        int integerValue = default;

        if (!int.TryParse(args.Filter, out integerValue) ||
            integerValue < 0)
        {
            ClinicIds = null;
            await InvokeAsync(StateHasChanged);
            return;
        }

        ClinicIds = await _clinicService
            .DataGridFilterIdAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }

    protected async Task LoadDataClinicsFilterName(LoadDataArgs args)
    {
        string filterValue = args.Filter;

        ClinicNames = await _clinicService
            .DataGridFilterNameAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }
}