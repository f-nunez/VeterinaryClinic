using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorById;

public class GetDoctorByIdRequest : BaseRequest
{
    public int DoctorId { get; set; }
}