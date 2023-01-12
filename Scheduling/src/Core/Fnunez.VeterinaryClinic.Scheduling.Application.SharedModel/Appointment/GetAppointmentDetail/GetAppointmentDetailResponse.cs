using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;

public class GetAppointmentDetailResponse : BaseResponse
{
    public AppointmentDetailDto? Appointment { get; set; }

    public GetAppointmentDetailResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}