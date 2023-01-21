using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Patients.Queries.GetPatientsFilterClient;

public class GetPatientsFilterClientQueryValidator
    : AbstractValidator<GetPatientsFilterClientQuery>
{
    public GetPatientsFilterClientQueryValidator()
    {
        RuleFor(v => v.GetPatientsFilterClientRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must be less or equals than 200 characters.");
    }
}