using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterId;

public class ClientIdsSpecification : BaseSpecification<Client, string>
{
    public ClientIdsSpecification(string idFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(c => c.IsActive)
            .Where(c => c.Id.ToString().Contains(idFilterValue.Trim()));

        Query
            .Select(c => $"{c.Id}");
    }
}