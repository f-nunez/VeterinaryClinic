using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterFullName;

public class ClientFullNamesSpecification : BaseSpecification<Client, string>
{
    public ClientFullNamesSpecification(string fullNameFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(c => c.FullName.Trim().ToLower().Contains(
                fullNameFilterValue.Trim().ToString()))
            .OrderBy(c => c.FullName)
            .Take(10);

        Query
            .Select(c => c.FullName);
    }
}