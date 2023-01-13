using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterEmailAddress;

public class GetClinicsFilterEmailAddressQueryValidator
    : AbstractValidator<GetClinicsFilterEmailAddressQuery>
{
    public GetClinicsFilterEmailAddressQueryValidator()
    {
        RuleFor(v => v.GetClinicsFilterEmailAddressRequest.EmailAddressFilterValue)
            .NotNull().WithMessage("EmailAddressFilterValue is required.")
            .NotEmpty().WithMessage("EmailAddressFilterValue is required.");
    }
}