using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientById;

public class GetPatientByIdQueryValidator : AbstractValidator<GetPatientByIdQuery>
{
    public GetPatientByIdQueryValidator()
    {
        RuleFor(v => v.GetPatientByIdRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");

        RuleFor(v => v.GetPatientByIdRequest.PatientId)
            .GreaterThan(0).WithMessage("PatientId is required.");
    }
}