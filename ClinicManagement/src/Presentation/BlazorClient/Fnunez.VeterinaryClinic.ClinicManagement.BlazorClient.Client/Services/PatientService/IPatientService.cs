using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface IPatientService
{
    public Task<PatientDto> CreateAsync(CreatePatientRequest createPatientRequest);
    public Task DeleteAsync(int clientId, int patientId);
    public Task<PatientDto> EditAsync(UpdatePatientRequest updatePatientRequest);
    public Task<PatientDto> GetByIdAsync(int patientId);
}