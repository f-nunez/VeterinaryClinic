using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;

public class GetAppointmentDetailRequest : BaseRequest
{
    public Guid AppointmentId { get; set; }
}