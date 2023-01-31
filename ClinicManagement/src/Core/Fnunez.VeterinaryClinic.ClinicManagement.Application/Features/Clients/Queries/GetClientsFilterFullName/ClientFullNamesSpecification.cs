using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterFullName;

public class ClientFullNamesSpecification : BaseSpecification<Client, string>
{
    public ClientFullNamesSpecification(string fullNameFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(c => c.IsActive)
            .Where(c => c.FullName.Trim().ToLower().Contains(
                fullNameFilterValue.Trim().ToString()))
            .OrderBy(c => c.FullName)
            .Take(10);

        Query
            .Select(c => c.FullName);
    }
}