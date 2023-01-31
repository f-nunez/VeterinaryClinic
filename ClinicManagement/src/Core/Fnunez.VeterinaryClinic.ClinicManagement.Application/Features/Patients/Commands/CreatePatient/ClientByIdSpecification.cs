using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;

public class ClientByIdSpecification : BaseSpecification<Client>
{
    public ClientByIdSpecification(int clientId)
    {
        Query
            .Include(c => c.Patients)
            .Where(c => c.Id == clientId);
    }
}