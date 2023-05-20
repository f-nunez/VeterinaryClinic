using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Clients;
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

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    [Inject]
    private IStringLocalizer<AddEditClientComponent> _stringLocalizerForAdd { get; set; }

    [Inject]
    private IStringLocalizer<ClientDetailComponent> _stringLocalizerForDetail { get; set; }

    [Inject]
    private IStringLocalizer<ClientsFilterComponent> _stringLocalizerForFilter { get; set; }

    protected bool CanWrite { get; set; }

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
            SalutationFilterValue = SalutationFilterValue
        };

        request.DataGridRequest.Search = SearchFilterValue;

        var dataGridResponse = await _clientService
            .DataGridAsync(request);

        Clients = dataGridResponse.Items;
        Count = dataGridResponse.Count;
        _spinnerService.Hide();

        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnClickAdd()
    {
        var response = await _dialogService.OpenAsync<AddEditClient>(
            _stringLocalizerForAdd["AddEditClient_Label_Add"],
            new Dictionary<string, object>
            {
                { "IsClientToAdd", true }
            }
        );

        if (response is null)
            return;

        var savedClient = response as AddEditClientVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Clients_AddedClient_Alert_Message"], savedClient.FullName),
            StringLocalizer["Clients_AddedClient_Alert_Title"],
            StringLocalizer["Clients_AddedClient_Alert_Button_Ok"]);

        await ClientsGrid.Reload();
    }

    protected async Task OnClickDelete(ClientDto client)
    {
        string message = string.Format(
            StringLocalizer["Clients_DeleteClient_Alert_Message"],
            client.FullName);

        bool? proceedToDelete = await _dialogService.Confirm(
            message,
            StringLocalizer["Clients_DeleteClient_Alert_Title"],
            new ConfirmOptions
            {
                OkButtonText = StringLocalizer["Clients_DeleteClient_Alert_Button_Ok"],
                CancelButtonText = StringLocalizer["Clients_DeleteClient_Alert_Button_Cancel"]
            }
        );

        if (!proceedToDelete.HasValue || !proceedToDelete.Value)
            return;

        _spinnerService.Show();

        var request = new DeleteClientRequest
        {
            Id = client.ClientId
        };

        await _clientService.DeleteAsync(request);

        _spinnerService.Hide();

        await ShowAlertAsync(
            string.Format(StringLocalizer["Clients_DeletedClient_Alert_Message"], client.FullName),
            StringLocalizer["Clients_DeletedClient_Alert_Title"],
            StringLocalizer["Clients_DeletedClient_Alert_Button_Ok"]);

        await ClientsGrid.ReloadAfterDeleteItemAsync();
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

    protected async Task OnClickEdit(ClientDto client)
    {
        _spinnerService.Show();

        var request = new GetClientEditRequest
        {
            ClientId = client.ClientId
        };

        var currentClientData = await _clientService
            .GetClientEditAsync(request);

        var clientToEdit = ClientHelper
            .MapAddEditClientViewModel(currentClientData.Client);

        _spinnerService.Hide();

        var response = await _dialogService.OpenAsync<AddEditClient>(
            _stringLocalizerForAdd["AddEditClient_Label_Edit"],
            new Dictionary<string, object>
            {
                { "IsClientToAdd", false },
                { "Model", clientToEdit },
                {
                    "PreselectedPreferredDoctorFilterValues",
                    currentClientData.PreferredDoctorFilterValues
                }
            }
        );

        if (response is null)
            return;

        var savedClient = response as AddEditClientVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Clients_EditedClient_Alert_Message"], savedClient.FullName),
            StringLocalizer["Clients_EditedClient_Alert_Title"],
            StringLocalizer["Clients_EditedClient_Alert_Button_Ok"]);

        await ClientsGrid.Reload();
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

    private async Task<bool?> ShowAlertAsync(
        string alertMessage,
        string alertTitle,
        string alertButtonOkMessage)
    {
        return await _dialogService.Alert(
            alertMessage,
            alertTitle,
            new AlertOptions
            {
                OkButtonText = alertButtonOkMessage
            }
        );
    }
}