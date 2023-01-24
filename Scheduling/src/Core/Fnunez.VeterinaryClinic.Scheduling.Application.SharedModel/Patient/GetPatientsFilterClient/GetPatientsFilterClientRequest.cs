using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient.GetPatientsFilterClient;

public class GetPatientsFilterClientRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
}