using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.DeleteDoctor;

public class DeleteDoctorCommandValidator
    : AbstractValidator<DeleteDoctorCommand>
{
    public DeleteDoctorCommandValidator()
    {
        RuleFor(v => v.DeleteDoctorRequest.Id)
            .GreaterThan(0).WithMessage("Id is required.");
    }
}