using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.UpdateAppointmentType;

public class UpdateAppointmentTypeCommandValidator
    : AbstractValidator<UpdateAppointmentTypeCommand>
{
    public UpdateAppointmentTypeCommandValidator()
    {
        RuleFor(v => v.UpdateAppointmentTypeRequest.Code)
            .NotEmpty().WithMessage("Code is required.")
            .NotNull().WithMessage("Code is required.")
            .MaximumLength(100).WithMessage("Code must not exceed 100 characters.");

        RuleFor(v => v.UpdateAppointmentTypeRequest.Duration)
            .GreaterThan(0).WithMessage("Duration must be greater than 0.");

        RuleFor(v => v.UpdateAppointmentTypeRequest.Id)
            .GreaterThan(0).WithMessage("Id is required.");

        RuleFor(v => v.UpdateAppointmentTypeRequest.Name)
            .NotEmpty().WithMessage("Name is required.")
            .NotNull().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");
    }
}