using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterEmailAddress;

public class ClientEmailAddressesSpecification
    : BaseSpecification<Client, string>
{
    public ClientEmailAddressesSpecification(string emailAddressFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(c => c.IsActive)
            .Where(c => c.EmailAddress.Trim().ToLower().Contains(
                emailAddressFilterValue.Trim().ToLower()))
            .OrderBy(c => c.EmailAddress)
            .Take(10);

        Query
            .Select(c => c.EmailAddress);
    }
}