using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Models.Clients;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels;
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

    protected int? PreferredLanguageValue { get; set; }

    protected List<DropDownValue> PreferredLanguageDropDownValues { get; set; }

    [Inject]
    protected IStringLocalizer<AddEditClientComponent> StringLocalizer { get; set; }

    [Parameter]
    public bool IsClientToAdd { get; set; }

    [Parameter]
    public AddEditClientVm Model { get; set; } = new();

    #region PreferredDoctor properties
    protected RadzenDropDownDataGrid<int?> PreferredDoctorDropDownDataGrid;

    protected int PreferredDoctorFilterCount;

    protected List<PreferredDoctorFilterValueDto> PreferredDoctorFilterValues = new();

    [Parameter]
    public List<PreferredDoctorFilterValueDto> PreselectedPreferredDoctorFilterValues { get; set; }
    #endregion

    protected override void OnInitialized()
    {
        PreferredLanguageDropDownValues = new List<DropDownValue>
        {
            new DropDownValue
            {
                Text = StringLocalizer["AddEditClient_PreferredLanguage_English"],
                Value = (int)PreferredLanguage.English
            },
            new DropDownValue
            {
                Text = StringLocalizer["AddEditClient_PreferredLanguage_Spanish"],
                Value = (int)PreferredLanguage.Spanish
            }
        };
    }

    protected override void OnParametersSet()
    {
        if (!IsClientToAdd)
        {
            PreferredDoctorFilterValues = PreselectedPreferredDoctorFilterValues;
            PreferredLanguageValue = Model.PreferredLanguage;
        }
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
            PreferredLanguage = Model.PreferredLanguage,
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
            PreferredLanguage = Model.PreferredLanguage,
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

    #region PreferredLanguage dropdown methods
    protected void OnChangePreferredLanguageDropDown(object value)
    {
        var convertedValue = value as Nullable<int>;

        Model.PreferredLanguage = convertedValue ?? 0;
    }
    #endregion
}