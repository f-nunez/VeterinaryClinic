using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctors;

public class GetDoctorsResponse : BaseResponse
{
    public List<DoctorDto> Doctors { get; set; } = new();
    public int Count { get; set; }

    public GetDoctorsResponse(Guid correlationId) : base(correlationId)
    {
    }
}