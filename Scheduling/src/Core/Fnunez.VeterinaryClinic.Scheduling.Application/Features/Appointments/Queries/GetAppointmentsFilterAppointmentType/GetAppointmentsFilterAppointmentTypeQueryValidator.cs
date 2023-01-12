using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterAppointmentType;

public class GetAppointmentsFilterAppointmentTypeQueryValidator
    : AbstractValidator<GetAppointmentsFilterAppointmentTypeQuery>
{
    public GetAppointmentsFilterAppointmentTypeQueryValidator()
    {
        RuleFor(v => v.GetAppointmentsFilterAppointmentTypeRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must be less or equals than 200 characters.");
    }
}