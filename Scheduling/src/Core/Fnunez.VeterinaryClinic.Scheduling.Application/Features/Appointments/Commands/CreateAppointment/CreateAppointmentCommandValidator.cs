using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandValidator
    : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(v => v.CreateAppointmentRequest.AppointmentTypeId)
            .GreaterThan(0).WithMessage("AppointmentTypeId is required.");

        RuleFor(v => v.CreateAppointmentRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");

        RuleFor(v => v.CreateAppointmentRequest.DoctorId)
            .GreaterThan(0).WithMessage("DoctorId is required.");

        RuleFor(v => v.CreateAppointmentRequest.DateOfAppointment)
            .NotNull().WithMessage("DateOfAppointment is required.");

        RuleFor(v => v.CreateAppointmentRequest.PatientId)
            .GreaterThan(0).WithMessage("PatientId is required.");

        RuleFor(v => v.CreateAppointmentRequest.RoomId)
            .GreaterThan(0).WithMessage("RoomId is required.");

        RuleFor(v => v.CreateAppointmentRequest.RoomId)
            .GreaterThan(0).WithMessage("RoomId is required.");

        RuleFor(v => v.CreateAppointmentRequest.ScheduleId)
            .NotEmpty().WithMessage("ScheduleId is required.");

        RuleFor(v => v.CreateAppointmentRequest.Title)
            .NotNull().WithMessage("Title is required.")
            .NotEmpty().WithMessage("Title is required.");
    }
}