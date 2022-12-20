using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentById;

public class GetAppointmentByIdQueryValidator
    : AbstractValidator<GetAppointmentByIdQuery>
{
    public GetAppointmentByIdQueryValidator()
    {
        RuleFor(q => q.GetAppointmentByIdRequest.AppointmentId)
            .NotEmpty().WithMessage("AppointmentId is required.");

        RuleFor(q => q.GetAppointmentByIdRequest.ScheduleId)
            .NotEmpty().WithMessage("ScheduleId is required.");
    }
}