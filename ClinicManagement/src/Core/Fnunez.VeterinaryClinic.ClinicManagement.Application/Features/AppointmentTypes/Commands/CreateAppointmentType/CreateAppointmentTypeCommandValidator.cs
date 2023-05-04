using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.CreateAppointmentType;

public class CreateAppointmentTypeCommandValidator
    : AbstractValidator<CreateAppointmentTypeCommand>
{
    public CreateAppointmentTypeCommandValidator()
    {
        RuleFor(v => v.CreateAppointmentTypeRequest.Code)
            .NotEmpty().WithMessage("Code is required.")
            .NotNull().WithMessage("Code is required.")
            .MaximumLength(100).WithMessage("Code must not exceed 100 characters.");

        RuleFor(v => v.CreateAppointmentTypeRequest.Duration)
            .GreaterThan(0).WithMessage("Duration must be greater than 0.");

        RuleFor(v => v.CreateAppointmentTypeRequest.Name)
            .NotEmpty().WithMessage("Name is required.")
            .NotNull().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");
    }
}