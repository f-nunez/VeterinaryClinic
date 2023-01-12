using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterClient;

public class GetAppointmentsFilterClientQueryValidator
    : AbstractValidator<GetAppointmentsFilterClientQuery>
{
    public GetAppointmentsFilterClientQueryValidator()
    {
        RuleFor(v => v.GetAppointmentsFilterClientRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must be less or equals than 200 characters.");
    }
}