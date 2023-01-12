using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.DeleteAppointment;

public class DeleteAppointmentCommandValidator
    : AbstractValidator<DeleteAppointmentCommand>
{
    public DeleteAppointmentCommandValidator()
    {
        RuleFor(v => v.DeleteAppointmentRequest.AppointmentId)
            .NotEmpty().WithMessage("AppointmentId is required.");
    }
}