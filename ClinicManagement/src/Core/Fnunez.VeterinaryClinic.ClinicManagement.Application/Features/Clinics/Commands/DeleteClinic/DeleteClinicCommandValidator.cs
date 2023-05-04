using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Commands.DeleteClinic;

public class DeleteClinicCommandValidator
    : AbstractValidator<DeleteClinicCommand>
{
    public DeleteClinicCommandValidator()
    {
        RuleFor(v => v.DeleteClinicRequest.Id)
            .GreaterThan(0).WithMessage("Id is required.");
    }
}