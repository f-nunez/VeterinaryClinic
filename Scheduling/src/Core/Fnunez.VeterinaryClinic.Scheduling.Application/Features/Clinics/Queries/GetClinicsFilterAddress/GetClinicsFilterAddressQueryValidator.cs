using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterAddress;

public class GetClinicsFilterAddressQueryValidator
    : AbstractValidator<GetClinicsFilterAddressQuery>
{
    public GetClinicsFilterAddressQueryValidator()
    {
        RuleFor(v => v.GetClinicsFilterAddressRequest.AddressFilterValue)
            .NotNull().WithMessage("AddressFilterValue is required.")
            .NotEmpty().WithMessage("AddressFilterValue is required.");
    }
}