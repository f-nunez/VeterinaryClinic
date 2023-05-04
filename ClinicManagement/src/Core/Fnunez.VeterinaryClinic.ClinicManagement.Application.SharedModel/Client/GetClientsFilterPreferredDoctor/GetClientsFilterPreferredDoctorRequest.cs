using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredDoctor;

public class GetClientsFilterPreferredDoctorRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
}