using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.UpdateAppointment;

public class AppointmentSpecification : BaseSpecification<Appointment>
{
    public AppointmentSpecification(Guid appointmentId)
    {
        Query
            .Include(a => a.Client)
            .Include(a => a.Clinic)
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Where(a => a.Id == appointmentId && a.IsActive);
    }
}