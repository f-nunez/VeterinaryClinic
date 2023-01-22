using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatients;

public class GetPatientsResponse : BaseResponse
{
    public List<PatientsDto> Patients { get; set; } = new List<PatientsDto>();
    public int Count { get; set; }

    public GetPatientsResponse(Guid correlationId) : base(correlationId)
    {
    }
}