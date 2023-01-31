using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterPreferredName;

public class ClientPreferredNamesSpecification : BaseSpecification<Client, string>
{
    public ClientPreferredNamesSpecification(string preferredNameFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(c => c.IsActive)
            .Where(c => c.PreferredName.Trim().ToLower().Contains(
                preferredNameFilterValue.Trim().ToLower()))
            .OrderBy(c => c.PreferredName)
            .Take(10);

        Query
            .Select(c => c.PreferredName);
    }
}