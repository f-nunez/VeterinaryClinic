using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Clients;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Clients;

public partial class AddEditClientComponent : ComponentBase
{
    [Inject]
    private IClientService _clientService { get; set; }

    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    protected bool IsSaving { get; set; }

    [Inject]
    protected IStringLocalizer<AddEditClientComponent> StringLocalizer { get; set; }

    [Parameter]
    public bool IsClientToAdd { get; set; }

    [Parameter]
    public ClientVm Model { get; set; } = new();

    #region PreferredDoctor properties
    protected RadzenDropDownDataGrid<int?> PreferredDoctorDropDownDataGrid;

    protected int PreferredDoctorFilterCount;

    protected List<PreferredDoctorFilterValueDto> PreferredDoctorFilterValues = new();

    [Parameter]
    public List<PreferredDoctorFilterValueDto> PreselectedPreferredDoctorFilterValues { get; set; }
    #endregion

    protected override void OnParametersSet()
    {
        if (!IsClientToAdd)
            PreferredDoctorFilterValues = PreselectedPreferredDoctorFilterValues;
    }

    protected async void OnSubmit()
    {
        _spinnerService.Show();

        IsSaving = true;

        if (IsClientToAdd)
            await CreateClientAsync();
        else
            await UpdateClientAsync();

        IsSaving = false;

        _spinnerService.Hide();

        _dialogService.Close(Model);
    }

    private async Task CreateClientAsync()
    {
        var request = new CreateClientRequest
        {
            EmailAddress = Model.EmailAddress,
            FullName = Model.FullName,
            PreferredDoctorId = Model.PreferredDoctorId,
            PreferredName = Model.PreferredName,
            Salutation = Model.Salutation
        };

        await _clientService.CreateAsync(request);
    }

    private async Task UpdateClientAsync()
    {
        var request = new UpdateClientRequest
        {
            ClientId = Model.ClientId,
            EmailAddress = Model.EmailAddress,
            FullName = Model.FullName,
            PreferredDoctorId = Model.PreferredDoctorId,
            PreferredName = Model.PreferredName,
            Salutation = Model.Salutation
        };

        await _clientService.UpdateAsync(request);
    }

    #region PreferredDoctor filter methods
    protected async Task PreferredDoctorFilterLoadData(LoadDataArgs args)
    {
        var request = new GetClientsFilterPreferredDoctorRequest
        {
            DataGridRequest = args.GetDataGridRequest()
        };

        var dataGridResponse = await _clientService
            .DataGridFilterPreferredDoctorAsync(request);

        PreferredDoctorFilterValues = dataGridResponse.Items;
        PreferredDoctorFilterCount = dataGridResponse.Count;

        await InvokeAsync(StateHasChanged);
    }

    protected void OnChangePreferredDoctorFilter(object value)
    {
        var convertedValue = value as Nullable<int>;
        if (convertedValue.HasValue && convertedValue.Value > 0)
        {
            Model.PreferredDoctorId = convertedValue.Value;
        }
        else
        {
            Model.PreferredDoctorId = null;
        }
    }
    #endregion
}