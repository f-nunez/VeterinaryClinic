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

        RuleFor(v => v.UpdateAppointmentRequest.RoomId)
            .GreaterThan(0).WithMessage("RoomId is required.");

        RuleFor(v => v.UpdateAppointmentRequest.ScheduleId)
            .NotEmpty().WithMessage("ScheduleId is required.");

        RuleFor(v => v.UpdateAppointmentRequest.StartOn)
            .NotNull().WithMessage("StartOn is required.");

        RuleFor(v => v.UpdateAppointmentRequest.Title)
            .NotNull().WithMessage("Title is required.")
            .NotEmpty().WithMessage("Title is required.");
    }
}