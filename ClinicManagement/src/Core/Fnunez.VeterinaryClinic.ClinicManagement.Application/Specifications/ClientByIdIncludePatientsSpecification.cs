using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;

public class ClientByIdIncludePatientsSpecification : BaseSpecification<Client>
{
    public ClientByIdIncludePatientsSpecification(int clientId)
    {
        Query.Where(client => client.Id == clientId);
        Query.Include(client => client.Patients);
    }
}