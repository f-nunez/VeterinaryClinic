using FluentValidation;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatients;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClients;

public class GetPatientsQueryValidator
    : AbstractValidator<GetPatientsQuery>
{
    public GetPatientsQueryValidator()
    {
        RuleFor(v => v.GetPatientsRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");
    }
}