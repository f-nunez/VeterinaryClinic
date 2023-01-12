using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterPatient;

public class ClientByIdIncludePatientsSpecification : BaseSpecification<Client>
{
    public ClientByIdIncludePatientsSpecification(int clientId)
    {
        Query
            .AsNoTracking()
            .Where(c => c.Id == clientId)
            .Include(c => c.Patients);
    }
}