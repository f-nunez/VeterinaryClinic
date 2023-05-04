using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Commands.DeletePatient;

public class DeletePatientCommandValidator
    : AbstractValidator<DeletePatientCommand>
{
    public DeletePatientCommandValidator()
    {
        RuleFor(v => v.DeletePatientRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");

        RuleFor(v => v.DeletePatientRequest.PatientId)
            .GreaterThan(0).WithMessage("PatientId is required.");
    }
}