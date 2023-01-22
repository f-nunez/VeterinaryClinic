using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientEdit;

public class GetPatientEditQueryValidator
    : AbstractValidator<GetPatientEditQuery>
{
    public GetPatientEditQueryValidator()
    {
        RuleFor(v => v.GetPatientEditRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");

        RuleFor(v => v.GetPatientEditRequest.PatientId)
            .GreaterThan(0).WithMessage("PatientId is required.");
    }
}