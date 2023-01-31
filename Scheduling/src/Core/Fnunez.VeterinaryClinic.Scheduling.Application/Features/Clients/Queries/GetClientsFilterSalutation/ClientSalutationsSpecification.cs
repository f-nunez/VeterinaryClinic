using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterSalutation;

public class ClientSalutationsSpecification : BaseSpecification<Client, string>
{
    public ClientSalutationsSpecification(string salutationFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(c => c.IsActive)
            .Where(c => c.Salutation.Trim().ToLower().Contains(
                salutationFilterValue.Trim().ToLower()))
            .OrderBy(c => c.Salutation)
            .Take(10);

        Query
            .Select(c => c.Salutation);
    }
}