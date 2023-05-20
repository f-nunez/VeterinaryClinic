using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointments;

public class GetAppointmentsQueryValidator
    : AbstractValidator<GetAppointmentsQuery>
{
    public GetAppointmentsQueryValidator()
    {
        RuleFor(v => v.GetAppointmentsRequest.ClientId)
            .GreaterThan(0).WithMessage("ClientId is required.");

        RuleFor(v => v.GetAppointmentsRequest.ClinicId)
            .GreaterThan(0).WithMessage("ClinicId is required.");

        RuleFor(v => v.GetAppointmentsRequest.EndOn)
            .NotNull().WithMessage("EndOn is required.")
            .NotEmpty().WithMessage("EndOn is required.");

        RuleFor(v => v.GetAppointmentsRequest.PatientId)
            .GreaterThan(0).WithMessage("PatientId is required.");

        RuleFor(v => v.GetAppointmentsRequest.StartOn)
            .NotNull().WithMessage("StartOn is required.")
            .NotEmpty().WithMessage("StartOn is required.")
            .LessThan(v => v.GetAppointmentsRequest.EndOn).WithMessage("StartOn must be less than EndOn.");
    }
}