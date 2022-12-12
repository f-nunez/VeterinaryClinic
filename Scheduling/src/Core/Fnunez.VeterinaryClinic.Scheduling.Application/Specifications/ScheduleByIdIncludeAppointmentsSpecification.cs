using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Specifications;

public class ScheduleByIdIncludeAppointmentsSpecification : BaseSpecification<Schedule>
{
    public ScheduleByIdIncludeAppointmentsSpecification(Guid id)
    {
        Query.Where(s => s.Id == id);
        Query.Include(s => s.Appointments);
    }
}