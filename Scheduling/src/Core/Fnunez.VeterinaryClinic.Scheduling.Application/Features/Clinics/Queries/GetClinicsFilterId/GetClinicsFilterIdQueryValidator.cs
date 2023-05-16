using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterId;

public class GetClinicsFilterIdQueryValidator
    : AbstractValidator<GetClinicsFilterIdQuery>
{
    public GetClinicsFilterIdQueryValidator()
    {
        RuleFor(v => v.GetClinicsFilterIdRequest.IdFilterValue)
            .NotNull().WithMessage("IdFilterValue is required.")
            .NotEmpty().WithMessage("IdFilterValue is required.")
            .MaximumLength(200).WithMessage("IdFilterValue must not exceed 200 characters.");
    }
}