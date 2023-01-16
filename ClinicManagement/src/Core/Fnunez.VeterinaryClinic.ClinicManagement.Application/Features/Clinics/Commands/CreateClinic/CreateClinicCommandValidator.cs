using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.CreateClinic;

public class CreateClinicCommandValidator
    : AbstractValidator<CreateClinicCommand>
{
    public CreateClinicCommandValidator()
    {
        RuleFor(v => v.CreateClinicRequest.Address)
            .NotNull().WithMessage("Address is required.")
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(1000).WithMessage("Address must not exceed 1000 characters.");

        RuleFor(v => v.CreateClinicRequest.EmailAddress)
            .NotNull().WithMessage("EmailAddress is required.")
            .NotEmpty().WithMessage("EmailAddress is required.")
            .MaximumLength(320).WithMessage("EmailAddress must not exceed 320 characters.");

        RuleFor(v => v.CreateClinicRequest.Name)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");
    }
}