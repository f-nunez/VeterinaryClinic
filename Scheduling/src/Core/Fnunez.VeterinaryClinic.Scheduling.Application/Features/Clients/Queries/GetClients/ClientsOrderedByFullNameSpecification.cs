using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClients;

public class ClientsOrderedByFullNameSpecification : BaseSpecification<Client>
{
    public ClientsOrderedByFullNameSpecification()
    {
        AddOrderBy(client => client.FullName);
    }
}