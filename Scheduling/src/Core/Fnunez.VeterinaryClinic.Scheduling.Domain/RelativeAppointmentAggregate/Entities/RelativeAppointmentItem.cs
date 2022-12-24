using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.RelativeAppointmentAggregate.Entities;

public class RelativeAppointmentItem : BaseEntity<Guid>, IAggregateRoot
{
    public Guid AppointerId { get; set; }
    public Guid AppointmentId { get; private set; }
    public Guid ScheduleId { get; private set; }

    public RelativeAppointmentItem(
        Guid id,
        Guid appointerId,
        Guid appointmentId,
        Guid scheduleId)
    {
        if (id == Guid.Empty)
            throw new ArgumentException(
                $"Required input {nameof(id)} cannot be empty.",
                nameof(id));

        if (appointerId == Guid.Empty)
            throw new ArgumentException(
                $"Required input {nameof(appointerId)} cannot be empty.",
                nameof(appointerId));

        if (appointmentId == Guid.Empty)
            throw new ArgumentException(
                $"Required input {nameof(appointmentId)} cannot be empty.",
                nameof(appointmentId));

        if (scheduleId == Guid.Empty)
            throw new ArgumentException(
                $"Required input {nameof(scheduleId)} cannot be empty.",
                nameof(scheduleId));
    }
}