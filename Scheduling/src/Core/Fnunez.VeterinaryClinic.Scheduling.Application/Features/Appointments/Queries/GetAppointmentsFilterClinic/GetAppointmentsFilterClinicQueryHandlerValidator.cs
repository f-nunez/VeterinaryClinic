using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterClinic;

public class GetAppointmentsFilterClinicQueryHandlerValidator
    : AbstractValidator<GetAppointmentsFilterClinicQuery>
{
    public GetAppointmentsFilterClinicQueryHandlerValidator()
    {
        RuleFor(v => v.GetAppointmentsFilterClinicRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must not exceed 200 characters.");

        RuleFor(v => v.GetAppointmentsFilterClinicRequest.DataGridRequest.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to 0.");

        RuleFor(v => v.GetAppointmentsFilterClinicRequest.DataGridRequest.Take)
            .GreaterThan(0).WithMessage("Take must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Take must be less than or equal to 100.");
    }
}