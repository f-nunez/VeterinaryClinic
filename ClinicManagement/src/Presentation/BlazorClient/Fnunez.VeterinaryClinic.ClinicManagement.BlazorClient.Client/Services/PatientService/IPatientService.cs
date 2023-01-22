using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientEdit;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface IPatientService
{
    public Task CreateAsync(CreatePatientRequest request);
    public Task<DataGridResponse<ClientFilterValueDto>> DataGridFilterClientAsync(GetPatientsFilterClientRequest request);
    public Task<DataGridResponse<PreferredDoctorFilterValueDto>> DataGridFilterPreferredDoctorAsync(GetPatientsFilterPreferredDoctorRequest request);
    public Task DeleteAsync(DeletePatientRequest request);
    public Task<PatientDto> GetByIdAsync(int patientId);
    public Task<GetPatientDetailResponse> GetPatientDetailAsync(GetPatientDetailRequest request);
    public Task<GetPatientEditResponse> GetPatientEditAsync(GetPatientEditRequest request);
    public Task<List<PatientsDto>> GetPatientsAsync(GetPatientsRequest request);
    public Task UpdateAsync(UpdatePatientRequest request);
}