using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Patients.Queries.GetPatientsFilterClient;

public class GetPatientsFilterClientQueryValidator
    : AbstractValidator<GetPatientsFilterClientQuery>
{
    public GetPatientsFilterClientQueryValidator()
    {
        RuleFor(v => v.GetPatientsFilterClientRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must not exceed 200 characters.");

        RuleFor(v => v.GetPatientsFilterClientRequest.DataGridRequest.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to 0.");

        RuleFor(v => v.GetPatientsFilterClientRequest.DataGridRequest.Take)
            .GreaterThan(0).WithMessage("Take must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Take must be less than or equal to 100.");
    }
}