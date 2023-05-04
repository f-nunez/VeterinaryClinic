using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientDetail;

public class GetPatientDetailRequest : BaseRequest
{
    public int ClientId { get; set; }
    public int PatientId { get; set; }
}