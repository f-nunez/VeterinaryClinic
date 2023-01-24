using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentAdd;

public class ClientByIdSpecification : BaseSpecification<Client>
{
    public ClientByIdSpecification(int clientId)
    {
        Query
            .AsNoTracking()
            .Include(c => c.Patients)
            .Where(c => c.Id == clientId);
    }
}