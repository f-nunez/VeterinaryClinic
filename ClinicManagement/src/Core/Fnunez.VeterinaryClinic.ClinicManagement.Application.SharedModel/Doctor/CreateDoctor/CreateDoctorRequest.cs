using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;

public class CreateDoctorRequest : BaseRequest
{
    public string FullName { get; set; } = string.Empty;
}