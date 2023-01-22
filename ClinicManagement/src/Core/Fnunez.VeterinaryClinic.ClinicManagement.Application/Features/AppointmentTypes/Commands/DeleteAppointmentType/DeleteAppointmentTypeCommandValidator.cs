using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.DeleteAppointmentType;

public class DeleteAppointmentTypeCommandValidator
    : AbstractValidator<DeleteAppointmentTypeCommand>
{
    public DeleteAppointmentTypeCommandValidator()
    {
        RuleFor(v => v.DeleteAppointmentTypeRequest.Id)
            .GreaterThan(0).WithMessage("Id is required.");
    }
}