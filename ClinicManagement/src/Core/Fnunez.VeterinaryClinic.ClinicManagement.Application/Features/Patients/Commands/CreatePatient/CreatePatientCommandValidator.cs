using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.CreatePatient;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(v => v.CreatePatientRequest.Breed)
            .NotNull().WithMessage("Breed is required.")
            .NotEmpty().WithMessage("Breed is required.")
            .MaximumLength(200).WithMessage("Breed must not exceed 200 characters.");

        RuleFor(v => v.CreatePatientRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");

        RuleFor(v => v.CreatePatientRequest.Name)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(v => v.CreatePatientRequest.PhotoData)
            .NotNull().WithMessage("PhotoData is required.")
            .NotEmpty().WithMessage("PhotoData is required.");

        RuleFor(v => v.CreatePatientRequest.PhotoName)
            .NotNull().WithMessage("PhotoName is required.")
            .NotEmpty().WithMessage("PhotoName is required.");

        RuleFor(v => v.CreatePatientRequest.Species)
            .NotNull().WithMessage("Species is required.")
            .NotEmpty().WithMessage("Species is required.");
    }
}