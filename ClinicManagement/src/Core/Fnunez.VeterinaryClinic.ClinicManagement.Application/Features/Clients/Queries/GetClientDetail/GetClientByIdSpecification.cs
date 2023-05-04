using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientDetail;

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