using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentEdit;

public class GetAppointmentEditRequest : BaseRequest
{
    public Guid AppointmentId { get; set; }
}