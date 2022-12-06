using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatients;

public class GetPatientsRequest : BaseRequest
{
    public int ClientId { get; set; }
}