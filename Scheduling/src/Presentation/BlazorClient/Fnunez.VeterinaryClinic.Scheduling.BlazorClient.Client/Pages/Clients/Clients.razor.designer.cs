using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Clients;

public partial class ClientsComponent : ComponentBase
{
    [Inject]
    private IClientService _clientService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    [Inject]
    private IStringLocalizer<ClientDetailComponent> _stringLocalizerForDetail { get; set; }

    [Inject]
    private IStringLocalizer<ClientsFilterComponent> _stringLocalizerForFilter { get; set; }

    protected RadzenDataGrid<ClientDto> ClientsGrid;

    protected List<ClientDto> Clients;

    protected int Count;

    [Inject]
    protected IStringLocalizer<ClientsComponent> StringLocalizer { get; set; }

    protected IEnumerable<int> PageSizeOptions = new int[] { 5, 10, 20, 30, 50, 100 };

    protected string EmailAddressFilterValue { get; set; }

    protected string FullNameFilterValue { get; set; }

    protected string IdFilterValue { get; set; }

    protected string PreferredNameFilterValue { get; set; }

    protected string SalutationFilterValue { get; set; }

    protected string SearchFilterValue { get; set; }

    protected async Task LoadData(LoadDataArgs args)
    {
        _spinnerService.Show();
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
        _spinnerService.Hide();

        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnClickDetail(ClientDto client)
    {
        _spinnerService.Show();
        var request = new GetClientDetailRequest
        {
            ClientId = client.ClientId
        };

        var currentClientData = await _clientService
            .GetClientDetailAsync(request);

        var clientDetail = ClientHelper
            .MapClientDetailViewModel(currentClientData.ClientDetail);

        _spinnerService.Hide();

        await _dialogService.OpenAsync<ClientDetail>(
            _stringLocalizerForDetail["ClientDetail_Label_ClientDetail"],
            new Dictionary<string, object>
            {
                { "Model", clientDetail }
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

        var result = await _dialogService.OpenSideAsync<ClientsFilter>(
            _stringLocalizerForFilter["ClientsFilter_Label_Filter"],
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