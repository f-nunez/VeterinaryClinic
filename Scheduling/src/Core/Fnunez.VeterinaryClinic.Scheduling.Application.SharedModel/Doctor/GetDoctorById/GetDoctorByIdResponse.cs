using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorById;

public class GetDoctorByIdResponse : BaseResponse
{
    public DoctorDto Doctor { get; set; } = new DoctorDto();

    public GetDoctorByIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}