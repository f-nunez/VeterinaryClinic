using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientCommandValidator()
    {
        RuleFor(v => v.UpdatePatientRequest.Breed)
            .NotNull().WithMessage("Breed is required.")
            .NotEmpty().WithMessage("Breed is required.")
            .MaximumLength(200).WithMessage("Breed must not exceed 200 characters.");

        RuleFor(v => v.UpdatePatientRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");

        RuleFor(v => v.UpdatePatientRequest.Name)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(v => v.UpdatePatientRequest.PatientId)
            .GreaterThan(0).WithMessage("PatientId is required.");

        RuleFor(v => v.UpdatePatientRequest.Species)
            .NotNull().WithMessage("Species is required.")
            .NotEmpty().WithMessage("Species is required.");

        When(v => v.UpdatePatientRequest.IsNewPhoto, () =>
        {
            RuleFor(v => v.UpdatePatientRequest.PhotoData)
                .NotNull().WithMessage("PhotoData is required.")
                .NotEmpty().WithMessage("PhotoData is required.");

            RuleFor(v => v.UpdatePatientRequest.PhotoName)
                .NotNull().WithMessage("PhotoName is required.")
                .NotEmpty().WithMessage("PhotoName is required.");
        });

    }
}