using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterPreferredName;

public class GetClientsFilterPreferredNameQueryValidator
    : AbstractValidator<GetClientsFilterPreferredNameQuery>
{
    public GetClientsFilterPreferredNameQueryValidator()
    {
        RuleFor(v => v.GetClientsFilterPreferredNameRequest.PreferredNameFilterValue)
            .NotNull().WithMessage("PreferredNameFilterValue is required")
            .NotEmpty().WithMessage("PreferredNameFilterValue is required")
            .MaximumLength(200).WithMessage("PreferredNameFilterValue must not exceed 200 characters.");
    }
}