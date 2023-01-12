using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterPatient;

public class GetAppointmentsFilterPatientQueryValidator
    : AbstractValidator<GetAppointmentsFilterPatientQuery>
{
    public GetAppointmentsFilterPatientQueryValidator()
    {
        RuleFor(v => v.GetAppointmentsFilterPatientRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");
    }
}