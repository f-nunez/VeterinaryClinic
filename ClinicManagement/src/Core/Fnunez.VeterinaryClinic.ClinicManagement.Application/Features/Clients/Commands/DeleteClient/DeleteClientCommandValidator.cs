using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientCommandValidator
    : AbstractValidator<DeleteClientCommand>
{
    public DeleteClientCommandValidator()
    {
        RuleFor(v => v.DeleteClientRequest.Id)
            .GreaterThan(0).WithMessage("Id is required.");
    }
}