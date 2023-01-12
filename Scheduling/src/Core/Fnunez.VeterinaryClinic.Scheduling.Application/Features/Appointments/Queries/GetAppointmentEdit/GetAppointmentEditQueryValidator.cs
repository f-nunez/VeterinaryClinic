using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentEdit;

public class GetAppointmentEditQueryValidator
    : AbstractValidator<GetAppointmentEditQuery>
{
    public GetAppointmentEditQueryValidator()
    {
        RuleFor(v => v.GetAppointmentEditRequest.AppointmentId)
            .NotEmpty().WithMessage("AppointmentId is required.");
    }
}