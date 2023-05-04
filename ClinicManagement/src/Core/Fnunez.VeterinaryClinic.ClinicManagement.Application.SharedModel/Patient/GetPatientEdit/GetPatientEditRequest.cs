using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientEdit;

public class GetPatientEditRequest : BaseRequest
{
    public int ClientId { get; set; }
    public int PatientId { get; set; }
}