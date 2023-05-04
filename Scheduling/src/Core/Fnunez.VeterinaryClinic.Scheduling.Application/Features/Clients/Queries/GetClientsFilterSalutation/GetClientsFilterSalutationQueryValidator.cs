using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterSalutation;

public class GetClientsFilterSalutationQueryValidator
    : AbstractValidator<GetClientsFilterSalutationQuery>
{
    public GetClientsFilterSalutationQueryValidator()
    {
        RuleFor(v => v.GetClientsFilterSalutationRequest.SalutationFilterValue)
            .NotNull().WithMessage("SalutationFilterValue is required.")
            .NotEmpty().WithMessage("SalutationFilterValue is required.");
    }
}