using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterEmailAddress;

public class GetClientsFilterEmailAddressQueryValidator
    : AbstractValidator<GetClientsFilterEmailAddressQuery>
{
    public GetClientsFilterEmailAddressQueryValidator()
    {
        RuleFor(v => v.GetClientsFilterEmailAddressRequest.EmailAddressFilterValue)
            .NotNull().WithMessage("EmailAddressFilterValue is required.")
            .NotEmpty().WithMessage("EmailAddressFilterValue is required.")
            .MaximumLength(200).WithMessage("EmailAddressFilterValue must not exceed 200 characters.");
    }
}