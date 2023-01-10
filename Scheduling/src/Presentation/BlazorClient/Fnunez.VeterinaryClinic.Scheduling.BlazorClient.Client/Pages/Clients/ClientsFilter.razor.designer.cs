using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Clients;

public partial class ClientsFilterComponent : ComponentBase
{
    [Inject]
    private IClientService _clientService { get; set; }

    protected IEnumerable<string> ClientEmailAddresses;

    protected IEnumerable<string> ClientFullNames;

    protected IEnumerable<string> ClientIds;

    protected IEnumerable<string> ClientPreferredNames;

    protected IEnumerable<string> ClientSalutations;

    [Inject]
    protected DialogService DialogService { get; set; }

    protected string EmailAddressFilterValue { get; set; }

    protected string FullNameFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    protected string PreferredNameFilterValue { get; set; }

    protected string SalutationFilterValue { get; set; }
    
    [Parameter]
    public ClientsFilterValues ClientsFilterValues { get; set; }

    protected override void OnInitialized()
    {
        EmailAddressFilterValue = ClientsFilterValues.EmailAddressFilterValue;
        FullNameFilterValue = ClientsFilterValues.FullNameFilterValue;
        IdFilterValue = ClientsFilterValues.IdFilterValue;
        PreferredNameFilterValue = ClientsFilterValues.PreferredNameFilterValue;
        SalutationFilterValue = ClientsFilterValues.SalutationFilterValue;
    }

    protected void OnClickButtonFilter()
    {
        var filterValues = new ClientsFilterValues
        {
            EmailAddressFilterValue = EmailAddressFilterValue,
            FullNameFilterValue = FullNameFilterValue,
            IdFilterValue = IdFilterValue,
            PreferredNameFilterValue = PreferredNameFilterValue,
            SalutationFilterValue = SalutationFilterValue
        };

        DialogService.CloseSide(filterValues);
    }

    protected void OnClickButtonClean()
    {
        var filterValues = new ClientsFilterValues
        {
            EmailAddressFilterValue = string.Empty,
            FullNameFilterValue = string.Empty,
            IdFilterValue = string.Empty,
            PreferredNameFilterValue = string.Empty,
            SalutationFilterValue = string.Empty
        };

        DialogService.CloseSide(filterValues);
    }

    protected async void OnChangeEmailAddress(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            ClientEmailAddresses = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async void OnChangeFullName(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            ClientFullNames = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async void OnChangeId(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            ClientIds = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async void OnChangePreferredName(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            ClientPreferredNames = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async void OnChangeSalutation(ChangeEventArgs changeEventArgs)
    {
        if (changeEventArgs.Value != null &&
            string.IsNullOrEmpty(changeEventArgs.Value.ToString()))
        {
            ClientSalutations = null;
            await InvokeAsync(StateHasChanged);
        }
    }

    protected async Task LoadDataClientsFilterEmailAddress(LoadDataArgs args)
    {
        string filterValue = args.Filter;

        ClientEmailAddresses = await _clientService
            .DataGridFilterEmailAddressAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }

    protected async Task LoadDataClientsFilterFullName(LoadDataArgs args)
    {
        string filterValue = args.Filter;

        ClientFullNames = await _clientService
            .DataGridFilterFullNameAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }

    protected async Task LoadDataClientsFilterId(LoadDataArgs args)
    {
        string filterValue = args.Filter;
        int integerValue = default;

        if (!int.TryParse(args.Filter, out integerValue) ||
            integerValue < 0)
        {
            ClientIds = null;
            await InvokeAsync(StateHasChanged);
            return;
        }

        ClientIds = await _clientService
            .DataGridFilterIdAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }

    protected async Task LoadDataClientsFilterPreferredName(LoadDataArgs args)
    {
        string filterValue = args.Filter;

        ClientPreferredNames = await _clientService
            .DataGridFilterPreferredNameAsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }

    protected async Task LoadDataClientsFilterSalutation(LoadDataArgs args)
    {
        string filterValue = args.Filter;

        ClientSalutations = await _clientService
            .DataGridFilterSalutationsync(filterValue);

        await InvokeAsync(StateHasChanged);
    }
}