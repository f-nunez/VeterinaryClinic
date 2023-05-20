using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterName;

public class GetClinicsFilterNameQueryValidator
    : AbstractValidator<GetClinicsFilterNameQuery>
{
    public GetClinicsFilterNameQueryValidator()
    {
        RuleFor(v => v.GetClinicsFilterNameRequest.NameFilterValue)
            .NotNull().WithMessage("NameFilterValue is required.")
            .NotEmpty().WithMessage("NameFilterValue is required.")
            .MaximumLength(200).WithMessage("NameFilterValue must not exceed 200 characters.");
    }
}