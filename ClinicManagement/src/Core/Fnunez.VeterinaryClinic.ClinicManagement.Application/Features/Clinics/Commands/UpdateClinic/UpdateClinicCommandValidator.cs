using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.UpdateClinic;

public class UpdateClinicCommandValidator
    : AbstractValidator<UpdateClinicCommand>
{
    public UpdateClinicCommandValidator()
    {
        RuleFor(v => v.UpdateClinicRequest.Address)
            .NotNull().WithMessage("Address is required.")
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(1000).WithMessage("Address must not exceed 1000 characters.");

        RuleFor(v => v.UpdateClinicRequest.EmailAddress)
            .NotNull().WithMessage("EmailAddress is required.")
            .NotEmpty().WithMessage("EmailAddress is required.")
            .MaximumLength(320).WithMessage("EmailAddress must not exceed 320 characters.");

        RuleFor(v => v.UpdateClinicRequest.Id)
            .GreaterThan(0).WithMessage("Id is required.");

        RuleFor(v => v.UpdateClinicRequest.Name)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");
    }
}