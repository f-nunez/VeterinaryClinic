using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterPreferredDoctor;

public class GetClientsFilterPreferredDoctorQueryValidator
    : AbstractValidator<GetClientsFilterPreferredDoctorQuery>
{
    public GetClientsFilterPreferredDoctorQueryValidator()
    {
        RuleFor(v => v.GetClientsFilterPreferredDoctorRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must be less or equals than 200 characters.");
    }
}