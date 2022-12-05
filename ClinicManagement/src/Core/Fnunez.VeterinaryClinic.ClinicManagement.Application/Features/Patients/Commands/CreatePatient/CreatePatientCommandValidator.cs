using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(v => v.CreatePatientRequest.PatientName)
            .NotNull().WithMessage("PatientName is required.")
            .NotEmpty().WithMessage("PatientName is required.")
            .MaximumLength(200).WithMessage("PatientName must not exceed 200 characters.");
    }
}