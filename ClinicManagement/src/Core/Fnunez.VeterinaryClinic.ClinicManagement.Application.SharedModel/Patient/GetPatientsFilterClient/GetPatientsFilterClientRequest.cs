using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterClient;

public class GetPatientsFilterClientRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
}