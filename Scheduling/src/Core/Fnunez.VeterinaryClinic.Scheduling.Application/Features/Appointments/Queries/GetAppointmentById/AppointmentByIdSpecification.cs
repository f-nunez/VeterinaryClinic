using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentById;

public class AppointmentByIdSpecification : BaseSpecification<Appointment>
{
    public AppointmentByIdSpecification(Guid appointmentId)
    {
        Query
            .AsNoTracking()
            .Include(a => a.AppointmentType)
            .Include(a => a.Client)
            .Include(a => a.Clinic)
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Where(s => s.Id == appointmentId);
    }
}