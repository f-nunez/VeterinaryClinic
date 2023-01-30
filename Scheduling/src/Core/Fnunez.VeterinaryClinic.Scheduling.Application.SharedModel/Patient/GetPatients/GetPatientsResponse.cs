using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatients;

public class GetPatientsResponse : BaseResponse
{
    public List<PatientsDto> Patients { get; set; } = new();
    public int Count { get; set; }

    public GetPatientsResponse(Guid correlationId) : base(correlationId)
    {
    }
}