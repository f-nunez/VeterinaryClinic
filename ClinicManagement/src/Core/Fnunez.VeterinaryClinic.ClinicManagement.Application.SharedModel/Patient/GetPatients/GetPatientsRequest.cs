using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatients;

public class GetPatientsRequest : BaseRequest
{
    public int ClientId { get; set; }
}