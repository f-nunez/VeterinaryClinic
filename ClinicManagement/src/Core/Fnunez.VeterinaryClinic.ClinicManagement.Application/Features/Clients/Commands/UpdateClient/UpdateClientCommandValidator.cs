using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.UpdateClient;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator()
    {
        RuleFor(v => v.UpdateClientRequest.EmailAddress)
            .NotNull().WithMessage("EmailAddress is required.")
            .NotEmpty().WithMessage("EmailAddress is required.")
            .MaximumLength(320).WithMessage("EmailAddress must not exceed 320 characters.");

        RuleFor(v => v.UpdateClientRequest.FullName)
            .NotNull().WithMessage("FullName is required.")
            .NotEmpty().WithMessage("FullName is required.")
            .MaximumLength(200).WithMessage("FullName must not exceed 200 characters.");

        RuleFor(v => v.UpdateClientRequest.PreferredName)
            .NotNull().WithMessage("PreferredName is required.")
            .NotEmpty().WithMessage("PreferredName is required.")
            .MaximumLength(200).WithMessage("PreferredName must not exceed 200 characters.");

        RuleFor(v => v.UpdateClientRequest.Salutation)
            .NotNull().WithMessage("Salutation is required.")
            .NotEmpty().WithMessage("Salutation is required.")
            .MaximumLength(200).WithMessage("Salutation must not exceed 200 characters.");
    }
}