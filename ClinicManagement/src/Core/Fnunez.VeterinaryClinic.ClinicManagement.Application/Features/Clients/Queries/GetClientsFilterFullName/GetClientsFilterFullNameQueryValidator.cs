using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterFullName;

public class GetClientsFilterFullNameQueryValidator
    : AbstractValidator<GetClientsFilterFullNameQuery>
{
    public GetClientsFilterFullNameQueryValidator()
    {
        RuleFor(v => v.GetClientsFilterFullNameRequest.FullNameFilterValue)
            .NotNull().WithMessage("FullNameFilterValue is required.")
            .NotEmpty().WithMessage("FullNameFilterValue is required.")
            .MaximumLength(200).WithMessage("FullNameFilterValue must not exceed 200 characters.");
    }
}