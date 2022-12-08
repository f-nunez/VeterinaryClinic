using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClients;

public class GetClientsOrderedByFullNameSpecification : BaseSpecification<Client>
{
    public GetClientsOrderedByFullNameSpecification()
    {
        Query.OrderBy(client => client.FullName);
    }
}