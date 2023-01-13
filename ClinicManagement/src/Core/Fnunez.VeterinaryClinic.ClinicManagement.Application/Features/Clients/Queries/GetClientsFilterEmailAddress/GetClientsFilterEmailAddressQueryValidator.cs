using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterEmailAddress;

public class GetClientsFilterEmailAddressQueryValidator
    : AbstractValidator<GetClientsFilterEmailAddressQuery>
{
    public GetClientsFilterEmailAddressQueryValidator()
    {
        RuleFor(v => v.GetClientsFilterEmailAddressRequest.EmailAddressFilterValue)
            .NotNull().WithMessage("EmailAddressFilterValue is required.")
            .NotEmpty().WithMessage("EmailAddressFilterValue is required.");
    }
}