using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientsFilterPreferredDoctor;

public class GetPatientsFilterPreferredDoctorQueryValidator
    : AbstractValidator<GetPatientsFilterPreferredDoctorQuery>
{
    public GetPatientsFilterPreferredDoctorQueryValidator()
    {
        RuleFor(v => v.GetPatientsFilterPreferredDoctorRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must not exceed 200 characters.");

        RuleFor(v => v.GetPatientsFilterPreferredDoctorRequest.DataGridRequest.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to 0.");

        RuleFor(v => v.GetPatientsFilterPreferredDoctorRequest.DataGridRequest.Take)
            .GreaterThan(0).WithMessage("Take must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Take must be less than or equal to 100.");
    }
}