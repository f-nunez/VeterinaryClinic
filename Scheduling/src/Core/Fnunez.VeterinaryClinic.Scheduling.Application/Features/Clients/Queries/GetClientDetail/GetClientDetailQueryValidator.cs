using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientDetail;

public class GetClientDetailQueryValidator
    : AbstractValidator<GetClientDetailQuery>
{
    public GetClientDetailQueryValidator()
    {
        RuleFor(v => v.GetClientDetailRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");
    }
}