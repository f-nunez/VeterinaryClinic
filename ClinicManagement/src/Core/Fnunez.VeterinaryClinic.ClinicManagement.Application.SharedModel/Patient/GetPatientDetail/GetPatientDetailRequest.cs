using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientDetail;

public class GetPatientDetailRequest : BaseRequest
{
    public int ClientId { get; set; }
    public int PatientId { get; set; }
}