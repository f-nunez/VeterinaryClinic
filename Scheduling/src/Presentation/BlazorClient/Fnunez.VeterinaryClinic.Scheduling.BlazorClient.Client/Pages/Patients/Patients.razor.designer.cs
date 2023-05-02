using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Patients;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Pages.Patients;

public partial class PatientsComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IPatientService _patientService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    [Inject]
    private IStringLocalizer<PatientDetailComponent> _stringLocalizerForDetail { get; set; }

    protected IEnumerable<int> PageSizeOptions = new int[] { 3, 6, 9, 18 };

    protected List<PatientsVm> Patients = new();

    protected RadzenDataList<PatientsVm> PatientsDataList;

    protected int Count { get; set; }

    protected bool IsLoading = false;

    protected bool IsEnabledAddButton => ClientId.HasValue;

    [Inject]
    protected IStringLocalizer<PatientsComponent> StringLocalizer { get; set; }

    #region Client filter properties
    protected List<ClientFilterValueDto> ClientFilterValues = new();

    protected int ClientFilterCount { get; set; }

    protected int? ClientId { get; set; }
    #endregion

    protected async Task OnClickDetail(PatientsVm patient)
    {
        _spinnerService.Show();

        var request = new GetPatientDetailRequest
        {
            ClientId = patient.ClientId,
            PatientId = patient.PatientId
        };

        var currentPatientData = await _patientService
            .GetPatientDetailAsync(request);

        var patientDetail = PatientHelper
            .MapPatientDetailViewModel(currentPatientData.PatientDetail);

        _spinnerService.Hide();

        await _dialogService.OpenAsync<PatientDetail>(
            _stringLocalizerForDetail["PatientDetail_Label_PatientDetail"],
            new Dictionary<string, object>
            {
                { "Model", patientDetail }
            }
        );
    }

    #region Client filter methods
    protected async Task ClientFilterLoadData(LoadDataArgs args)
    {
        _spinnerService.Show();

        var request = new GetPatientsFilterClientRequest
        {
            DataGridRequest = args.GetDataGridRequest()
        };

        var dataGridResponse = await _patientService
            .DataGridFilterClientAsync(request);

        ClientFilterValues = dataGridResponse.Items;
        ClientFilterCount = dataGridResponse.Count;

        _spinnerService.Hide();

        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnChangeClientFilter(object value)
    {
        var convertedValue = value as Nullable<int>;
        if (convertedValue.HasValue && convertedValue.Value > 0)
        {
            ClientId = convertedValue;

            var request = new GetPatientsRequest
            {
                ClientId = ClientId.Value
            };

            await GetPatientsAsync();
        }
        else
        {
            ClientId = null;
            Patients = new();
        }

        Count = Patients.Count;

        await InvokeAsync(StateHasChanged);
    }
    #endregion

    private async Task GetPatientsAsync()
    {
        _spinnerService.Show();

        var request = new GetPatientsRequest
        {
            ClientId = ClientId.Value
        };

        var patientDtos = await _patientService.GetPatientsAsync(request);

        Patients = PatientHelper.MapPatientsViewModels(patientDtos);

        _spinnerService.Hide();
    }
}