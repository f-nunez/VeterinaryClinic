using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterPreferredName;

public class GetClientsFilterPreferredNameQueryValidator
    : AbstractValidator<GetClientsFilterPreferredNameQuery>
{
    public GetClientsFilterPreferredNameQueryValidator()
    {
        RuleFor(v => v.GetClientsFilterPreferredNameRequest.PreferredNameFilterValue)
            .NotNull().WithMessage("PreferredNameFilterValue is required")
            .NotEmpty().WithMessage("PreferredNameFilterValue is required");
    }
}