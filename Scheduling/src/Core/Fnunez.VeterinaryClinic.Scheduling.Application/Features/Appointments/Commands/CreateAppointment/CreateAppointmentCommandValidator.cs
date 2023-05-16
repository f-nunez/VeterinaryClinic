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

        RuleFor(v => v.CreateAppointmentRequest.ClinicId)
            .GreaterThan(0).WithMessage("ClinicId is required.");

        RuleFor(v => v.CreateAppointmentRequest.Description)
            .NotNull().WithMessage("Description is required.")
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000).WithMessage("Description must be less or equals than 2000 characters.");

        RuleFor(v => v.CreateAppointmentRequest.DoctorId)
            .GreaterThan(0).WithMessage("DoctorId is required.");

        RuleFor(v => v.CreateAppointmentRequest.EndOn)
            .NotNull().WithMessage("EndOn is required.")
            .NotEmpty().WithMessage("EndOn is required.");

        RuleFor(v => v.CreateAppointmentRequest.PatientId)
            .GreaterThan(0).WithMessage("PatientId is required.");

        RuleFor(v => v.CreateAppointmentRequest.RoomId)
            .GreaterThan(0).WithMessage("RoomId is required.");

        RuleFor(v => v.CreateAppointmentRequest.StartOn)
            .NotNull().WithMessage("StartOn is required.")
            .NotEmpty().WithMessage("StartOn is required.")
            .LessThan(v => v.CreateAppointmentRequest.EndOn).WithMessage("StartOn must be less than EndOn.");

        RuleFor(v => v.CreateAppointmentRequest.Title)
            .NotNull().WithMessage("Title is required.")
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must be less or equals than 200 characters.");
    }
}