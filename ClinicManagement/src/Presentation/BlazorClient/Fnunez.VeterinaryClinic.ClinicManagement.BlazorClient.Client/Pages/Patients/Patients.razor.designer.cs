using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterClient;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Patients;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Patients;

public partial class PatientsComponent : ComponentBase
{
    [Inject]
    private DialogService _dialogService { get; set; }

    [Inject]
    private IPatientService _patientService { get; set; }

    [Inject]
    private ISpinnerService _spinnerService { get; set; }

    [Inject]
    private IStringLocalizer<AddEditPatientComponent> _stringLocalizerForAdd { get; set; }

    [Inject]
    private IStringLocalizer<PatientDetailComponent> _stringLocalizerForDetail { get; set; }

    protected bool CanWrite { get; set; }

    protected IEnumerable<int> PageSizeOptions = new int[] { 3, 6, 9, 18 };

    protected List<PatientsVm> Patients = new();

    protected RadzenDataList<PatientsVm> PatientsDataList;

    protected int Count { get; set; }

    protected bool IsEnabledAddButton => ClientId.HasValue;

    [Inject]
    protected IStringLocalizer<PatientsComponent> StringLocalizer { get; set; }

    #region Client filter properties
    protected List<ClientFilterValueDto> ClientFilterValues = new();

    protected int ClientFilterCount { get; set; }

    protected int? ClientId { get; set; }
    #endregion

    protected async Task OnClickAdd()
    {
        var response = await _dialogService.OpenAsync<AddEditPatient>(
            _stringLocalizerForAdd["AddEditPatient_Label_Add"],
            new Dictionary<string, object>
            {
                { "IsPatientToAdd", true },
                { "Model", new AddEditPatientVm { ClientId = ClientId.Value } }
            }
        );

        if (response is null)
            return;

        var savedPatient = response as AddEditPatientVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Patients_AddedPatient_Alert_Message"], savedPatient.Name),
            StringLocalizer["Patients_AddedPatient_Alert_Title"],
            StringLocalizer["Patients_AddedPatient_Alert_Button_Ok"]);

        await GetPatientsAsync();
    }

    protected async Task OnClickDelete(PatientsVm patient)
    {
        string message = string.Format(
            StringLocalizer["Patients_DeletePatient_Alert_Message"],
            patient.Name);

        bool? proceedToDelete = await _dialogService.Confirm(
            message,
            StringLocalizer["Patients_DeletePatient_Alert_Title"],
            new ConfirmOptions
            {
                OkButtonText = StringLocalizer["Patients_DeletePatient_Alert_Button_Ok"],
                CancelButtonText = StringLocalizer["Patients_DeletePatient_Alert_Button_Cancel"]
            }
        );

        if (!proceedToDelete.HasValue || !proceedToDelete.Value)
            return;

        _spinnerService.Show();

        var request = new DeletePatientRequest
        {
            ClientId = patient.ClientId,
            PatientId = patient.PatientId
        };

        await _patientService.DeleteAsync(request);

        _spinnerService.Hide();

        await ShowAlertAsync(
            string.Format(StringLocalizer["Patients_DeletedPatient_Alert_Message"], patient.Name),
            StringLocalizer["Patients_DeletedPatient_Alert_Title"],
            StringLocalizer["Patients_DeletedPatient_Alert_Button_Ok"]);

        await GetPatientsAsync();
    }

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

    protected async Task OnClickEdit(PatientsVm patient)
    {
        _spinnerService.Show();

        var request = new GetPatientEditRequest
        {
            ClientId = patient.ClientId,
            PatientId = patient.PatientId
        };

        var currentPatientData = await _patientService
            .GetPatientEditAsync(request);

        var patientToEdit = PatientHelper
            .MapAddEditPatientViewModel(currentPatientData.Patient);

        _spinnerService.Hide();

        var response = await _dialogService.OpenAsync<AddEditPatient>(
            _stringLocalizerForAdd["AddEditPatient_Label_Edit"],
            new Dictionary<string, object>
            {
                { "IsPatientToAdd", false },
                { "Model", patientToEdit },
                {
                    "PreselectedPreferredDoctorFilterValues",
                    currentPatientData.PreferredDoctorFilterValues
                }
            }
        );

        if (response is null)
            return;

        var savedPatient = response as AddEditPatientVm;

        await ShowAlertAsync(
            string.Format(StringLocalizer["Patients_EditedPatient_Alert_Message"], savedPatient.Name),
            StringLocalizer["Patients_EditedPatient_Alert_Title"],
            StringLocalizer["Patients_EditedPatient_Alert_Button_Ok"]);

        await GetPatientsAsync();
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