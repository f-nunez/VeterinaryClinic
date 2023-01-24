using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatientDetail;

public class GetPatientDetailQueryValidator
    : AbstractValidator<GetPatientDetailQuery>
{
    public GetPatientDetailQueryValidator()
    {
        RuleFor(v => v.GetPatientDetailRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");

        RuleFor(v => v.GetPatientDetailRequest.PatientId)
            .GreaterThan(0).WithMessage("PatientId is required.");
    }
}