using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterPreferredDoctor;

public class GetPatientsFilterPreferredDoctorRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
}