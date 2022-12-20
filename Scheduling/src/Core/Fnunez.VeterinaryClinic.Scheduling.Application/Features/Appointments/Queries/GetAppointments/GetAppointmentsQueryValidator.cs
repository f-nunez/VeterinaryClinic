using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointments;

public class GetAppointmentsQueryValidator
    : AbstractValidator<GetAppointmentsQuery>
{
    public GetAppointmentsQueryValidator()
    {
        RuleFor(q => q.GetAppointmentsRequest.ScheduleId)
            .NotEmpty().WithMessage("ScheduleId is required.");
    }
}