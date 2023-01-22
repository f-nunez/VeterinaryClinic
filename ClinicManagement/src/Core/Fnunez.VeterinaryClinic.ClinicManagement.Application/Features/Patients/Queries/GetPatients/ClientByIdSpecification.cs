using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatients;

public class ClientByIdSpecification : BaseSpecification<Client>
{
    public ClientByIdSpecification(int clientId)
    {
        Query.Where(client => client.Id == clientId);
        Query.Include(client => client.Patients);
    }
}