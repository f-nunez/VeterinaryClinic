using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatients;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientsFilterClient;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public interface IPatientService
{
    public Task<DataGridResponse<ClientFilterValueDto>> DataGridFilterClientAsync(GetPatientsFilterClientRequest request);
    public Task<GetPatientDetailResponse> GetPatientDetailAsync(GetPatientDetailRequest request);
    public Task<List<PatientsDto>> GetPatientsAsync(GetPatientsRequest request);
}