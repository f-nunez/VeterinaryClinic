using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointments;

public class GetAppointmentsQueryValidator
    : AbstractValidator<GetAppointmentsQuery>
{
    public GetAppointmentsQueryValidator()
    {
        RuleFor(v => v.GetAppointmentsRequest.ClientIdFilterValue)
            .NotNull().WithMessage("ClinicId is required.");
    }
}