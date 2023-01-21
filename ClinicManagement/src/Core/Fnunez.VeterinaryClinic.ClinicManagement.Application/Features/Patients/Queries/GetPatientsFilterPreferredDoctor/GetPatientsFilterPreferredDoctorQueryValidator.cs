using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientsFilterPreferredDoctor;

public class GetPatientsFilterPreferredDoctorQueryValidator
    : AbstractValidator<GetPatientsFilterPreferredDoctorQuery>
{
    public GetPatientsFilterPreferredDoctorQueryValidator()
    {
        RuleFor(v => v.GetPatientsFilterPreferredDoctorRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must be less or equals than 200 characters.");
    }
}