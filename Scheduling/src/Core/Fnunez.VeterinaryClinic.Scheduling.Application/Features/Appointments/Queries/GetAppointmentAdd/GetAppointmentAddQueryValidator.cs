using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentAdd;

public class GetAppointmentAddQueryValidator
    : AbstractValidator<GetAppointmentAddQuery>
{
    public GetAppointmentAddQueryValidator()
    {
        RuleFor(v => v.GetAppointmentAddRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");

        RuleFor(v => v.GetAppointmentAddRequest.ClinicId)
            .GreaterThan(0).WithMessage("ClinicId is required.");

        RuleFor(v => v.GetAppointmentAddRequest.PatientId)
            .GreaterThan(0).WithMessage("PatientId  is required.");
    }
}