using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentDetail;

public class GetAppointmentDetailQueryValidator
    : AbstractValidator<GetAppointmentDetailQuery>
{
    public GetAppointmentDetailQueryValidator()
    {
        RuleFor(v => v.GetAppointmentDetailRequest.AppointmentId)
            .NotEmpty().WithMessage("AppointmentId is required.");
    }
}