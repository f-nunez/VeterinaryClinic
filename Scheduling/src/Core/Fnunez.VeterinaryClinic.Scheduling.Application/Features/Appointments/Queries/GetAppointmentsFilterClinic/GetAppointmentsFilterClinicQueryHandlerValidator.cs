using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterClinic;

public class GetAppointmentsFilterClinicQueryHandlerValidator
    : AbstractValidator<GetAppointmentsFilterClinicQuery>
{
    public GetAppointmentsFilterClinicQueryHandlerValidator()
    {
        RuleFor(v => v.GetAppointmentsFilterClinicRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must be less or equals than 200 characters.");
    }
}