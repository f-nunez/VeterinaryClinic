using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointments.Specifications;

public class ScheduleByIdIncludeAppointmentsThenIncludeClientAndPatientSpecification : BaseSpecification<Schedule>
{
    public ScheduleByIdIncludeAppointmentsThenIncludeClientAndPatientSpecification(Guid id)
    {
        Query
            .AsNoTracking()
            .Include(s => s.Appointments)
            .ThenInclude(a => a.Client)
            .Include(s => s.Appointments)
            .ThenInclude(a => a.Patient)
            .Where(s => s.Id == id);
    }
}