using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientDetail;

public class GetClientByIdSpecification : BaseSpecification<Client>
{
    public GetClientByIdSpecification(int clientId)
    {
        Query
            .AsNoTracking()
            .Include(c => c.PreferredDoctor)
            .Where(c => c.Id == clientId);
    }
}