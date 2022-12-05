using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientCommandValidator()
    {
        RuleFor(v => v.UpdatePatientRequest.PatientName)
            .NotNull().WithMessage("PatientName is required.")
            .NotEmpty().WithMessage("PatientName is required.")
            .MaximumLength(200).WithMessage("PatientName must not exceed 200 characters.");
    }
}