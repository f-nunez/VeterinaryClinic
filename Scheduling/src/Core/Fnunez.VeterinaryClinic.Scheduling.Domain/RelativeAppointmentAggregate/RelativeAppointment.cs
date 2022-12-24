using Fnunez.VeterinaryClinic.Scheduling.Domain.RelativeAppointmentAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.RelativeAppointmentAggregate.Exceptions;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.RelativeAppointmentAggregate;

public class RelativeAppointment : BaseEntity<Guid>, IAggregateRoot
{
    private IList<RelativeAppointmentItem> _relativeAppointmentItems = new List<RelativeAppointmentItem>();
    public IReadOnlyList<RelativeAppointmentItem> RelativeAppointmentItems => _relativeAppointmentItems.AsReadOnly();

    public RelativeAppointment(Guid id)
    {
        if (id != Guid.Empty)
            throw new ArgumentException(
                $"Required input {nameof(id)} cannot be empty.",
                nameof(id));
    }

    public void AddSiblingAppointment(RelativeAppointmentItem siblingAppointment)
    {
        if (siblingAppointment is null)
            throw new ArgumentNullException(nameof(siblingAppointment));

        if (_relativeAppointmentItems.Any(a => a.Id == siblingAppointment.Id))
            throw new DuplicateRelativeAppointmentException(
                "Cannot add duplicate sibling appointment to appointer.",
                nameof(siblingAppointment));

        _relativeAppointmentItems.Add(siblingAppointment);
    }

    public void RemoveSiblingAppointment(RelativeAppointmentItem siblingAppointment)
    {
        if (siblingAppointment is null)
            throw new ArgumentNullException(nameof(siblingAppointment));

        _relativeAppointmentItems.Remove(siblingAppointment);
    }
}