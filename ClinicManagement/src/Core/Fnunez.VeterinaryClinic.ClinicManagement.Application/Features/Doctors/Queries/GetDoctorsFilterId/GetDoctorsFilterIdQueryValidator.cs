using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorsFilterId;

public class GetDoctorsFilterIdQueryValidator
    : AbstractValidator<GetDoctorsFilterIdQuery>
{
    public GetDoctorsFilterIdQueryValidator()
    {
        RuleFor(v => v.GetDoctorsFilterIdRequest.IdFilterValue)
            .NotNull().WithMessage("IdFilterValue is required.")
            .NotEmpty().WithMessage("IdFilterValue is required.")
            .MaximumLength(200).WithMessage("IdFilterValue must not exceed 200 characters.");
    }
}