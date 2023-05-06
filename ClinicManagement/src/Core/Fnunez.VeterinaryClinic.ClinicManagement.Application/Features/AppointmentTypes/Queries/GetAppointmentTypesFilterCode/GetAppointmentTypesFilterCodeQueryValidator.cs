using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterCode;

public class GetAppointmentTypesFilterCodeQueryValidator
    : AbstractValidator<GetAppointmentTypesFilterCodeQuery>
{
    public GetAppointmentTypesFilterCodeQueryValidator()
    {
        RuleFor(v => v.GetAppointmentTypesFilterCodeRequest.CodeFilterValue)
            .NotNull().WithMessage("CodeFilterValue is required.")
            .NotEmpty().WithMessage("CodeFilterValue is required.")
            .MaximumLength(200).WithMessage("CodeFilterValue must not exceed 200 characters.");
    }
}