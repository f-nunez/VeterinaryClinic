using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.UpdateDoctor;

public class UpdateDoctorCommandValidator
    : AbstractValidator<UpdateDoctorCommand>
{
    public UpdateDoctorCommandValidator()
    {
        RuleFor(v => v.UpdateDoctorRequest.FullName)
            .NotNull().WithMessage("FullName is required.")
            .NotEmpty().WithMessage("FullName is required.")
            .MaximumLength(200).WithMessage("FullName must not exceed 200 characters.");

        RuleFor(v => v.UpdateDoctorRequest.Id)
            .GreaterThan(0).WithMessage("Id is required.");
    }
}