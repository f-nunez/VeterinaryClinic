using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientEdit;

public class GetClientEditQueryValidator : AbstractValidator<GetClientEditQuery>
{
    public GetClientEditQueryValidator()
    {
        RuleFor(v => v.GetClientEditRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");
    }
}