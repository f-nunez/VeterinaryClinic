using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;

public class UpdateDoctorRequest : BaseRequest
{
    public int DoctorId { get; set; }
    public string? FullName { get; set; }
}