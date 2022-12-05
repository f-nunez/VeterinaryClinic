using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.CreateClient;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(v => v.CreateClientRequest.EmailAddress)
            .NotNull().WithMessage("EmailAddress is required.")
            .NotEmpty().WithMessage("EmailAddress is required.")
            .MaximumLength(320).WithMessage("EmailAddress must not exceed 320 characters.");

        RuleFor(v => v.CreateClientRequest.FullName)
            .NotNull().WithMessage("FullName is required.")
            .NotEmpty().WithMessage("FullName is required.")
            .MaximumLength(200).WithMessage("FullName must not exceed 200 characters.");

        RuleFor(v => v.CreateClientRequest.PreferredName)
            .NotNull().WithMessage("PreferredName is required.")
            .NotEmpty().WithMessage("PreferredName is required.")
            .MaximumLength(200).WithMessage("PreferredName must not exceed 200 characters.");

        RuleFor(v => v.CreateClientRequest.Salutation)
            .NotNull().WithMessage("Salutation is required.")
            .NotEmpty().WithMessage("Salutation is required.")
            .MaximumLength(200).WithMessage("Salutation must not exceed 200 characters.");
    }
}