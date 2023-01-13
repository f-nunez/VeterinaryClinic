using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterId;

public class ClientIdsSpecification : BaseSpecification<Client, string>
{
    public ClientIdsSpecification(string idFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(c => c.Id.ToString().Contains(idFilterValue.Trim()));

        Query
            .Select(c => $"{c.Id}");
    }
}