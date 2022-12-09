using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatients;

public class ClientByIdIncludePatientsSpecification : BaseSpecification<Client>
{
    public ClientByIdIncludePatientsSpecification(int clientId)
    {
        Query.Where(client => client.Id == clientId);
        Query.Include(client => client.Patients);
    }
}