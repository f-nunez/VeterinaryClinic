using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Clients;

public partial class ClientsComponent : ComponentBase
{
    [Inject]
    private IClientService _clientService { get; set; }

    protected RadzenDataGrid<ClientDto> ClientsGrid;

    protected List<ClientDto> Clients;

    protected int Count;

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected IStringLocalizer<ClientsComponent> StringLocalizer { get; set; }

    [Inject]
    protected IStringLocalizer<ClientsFilterComponent> StringLocalizerForFilter { get; set; }

    protected bool IsLoading = false;

    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };

    protected string EmailAddressFilterValue { get; set; }

    protected string FullNameFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    protected string PreferredNameFilterValue { get; set; }

    protected string SalutationFilterValue { get; set; }

    protected string SearchFilterValue { get; set; }

    protected async Task LoadData(LoadDataArgs args)
    {
        IsLoading = true;
        var request = new GetClientsRequest
        {
            DataGridRequest = args.GetDataGridRequest(),
            EmailAddressFilterValue = EmailAddressFilterValue,
            FullNameFilterValue = FullNameFilterValue,
            IdFilterValue = IdFilterValue,
            PreferredNameFilterValue = PreferredNameFilterValue,
            SalutationFilterValue = SalutationFilterValue,
            SearchFilterValue = SearchFilterValue
        };

        var dataGridResponse = await _clientService
            .DataGridAsync(request);

        Clients = dataGridResponse.Items;
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
        var filterValues = new ClientsFilterValues
        {
            EmailAddressFilterValue = EmailAddressFilterValue,
            FullNameFilterValue = FullNameFilterValue,
            IdFilterValue = IdFilterValue,
            PreferredNameFilterValue = PreferredNameFilterValue,
            SalutationFilterValue = SalutationFilterValue
        };

        var filterParameters = new Dictionary<string, object>()
        {
            { nameof(ClientsFilterValues), filterValues }
        };

        var result = await DialogService.OpenSideAsync<ClientsFilter>(
            StringLocalizerForFilter["ClientsFilter_Label_Filter"],
            filterParameters
        );

        await ProcessClosedFilterMenuAsync(result);
    }

    private async Task ProcessClosedFilterMenuAsync(ClientsFilterValues filterValues)
    {
        if (filterValues is null)
            return;

        EmailAddressFilterValue = filterValues.EmailAddressFilterValue;
        FullNameFilterValue = filterValues.FullNameFilterValue;
        IdFilterValue = filterValues.IdFilterValue;
        PreferredNameFilterValue = filterValues.PreferredNameFilterValue;
        SalutationFilterValue = filterValues.SalutationFilterValue;

        await ResetGridAndSearchAsync();
    }

    private async Task ResetGridAndSearchAsync()
    {
        ClientsGrid.Reset(false);
        await ClientsGrid.FirstPage(true);
    }
}