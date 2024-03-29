using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.UpdateAppointment;

public class UpdateAppointmentCommandValidator
    : AbstractValidator<UpdateAppointmentCommand>
{
    public UpdateAppointmentCommandValidator()
    {
        RuleFor(v => v.UpdateAppointmentRequest.AppointmentId)
            .NotEmpty().WithMessage("AppointmentId is required.");

        RuleFor(v => v.UpdateAppointmentRequest.AppointmentTypeId)
            .GreaterThan(0).WithMessage("AppointmentTypeId is required.");

        RuleFor(v => v.UpdateAppointmentRequest.DoctorId)
            .GreaterThan(0).WithMessage("DoctorId is required.");

        RuleFor(v => v.UpdateAppointmentRequest.Description)
            .NotNull().WithMessage("Description is required.")
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000).WithMessage("Description must be less or equals than 2000 characters.");

        RuleFor(v => v.UpdateAppointmentRequest.EndOn)
            .NotNull().WithMessage("EndOn is required.")
            .NotEmpty().WithMessage("EndOn is required.");

        RuleFor(v => v.UpdateAppointmentRequest.RoomId)
            .GreaterThan(0).WithMessage("RoomId is required.");

        RuleFor(v => v.UpdateAppointmentRequest.StartOn)
            .NotNull().WithMessage("StartOn is required.")
            .NotEmpty().WithMessage("StartOn is required.")
            .LessThan(v => v.UpdateAppointmentRequest.EndOn).WithMessage("StartOn must be less than EndOn.");

        RuleFor(v => v.UpdateAppointmentRequest.Title)
            .NotNull().WithMessage("Title is required.")
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must be less or equals than 2000 characters.");
    }
}