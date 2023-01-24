using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorById;

public class GetDoctorByIdRequest : BaseRequest
{
    public int Id { get; set; }
}