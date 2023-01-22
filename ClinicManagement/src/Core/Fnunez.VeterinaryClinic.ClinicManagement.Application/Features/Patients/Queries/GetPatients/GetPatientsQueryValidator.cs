using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatients;

public class GetPatientsQueryValidator : AbstractValidator<GetPatientsQuery>
{
    public GetPatientsQueryValidator()
    {
        RuleFor(v => v.GetPatientsRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");
    }
}