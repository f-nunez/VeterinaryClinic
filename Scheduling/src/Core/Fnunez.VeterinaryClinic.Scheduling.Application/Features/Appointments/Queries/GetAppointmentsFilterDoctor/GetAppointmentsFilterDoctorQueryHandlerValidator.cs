using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterDoctor;

public class GetAppointmentsFilterDoctorQueryHandlerValidator
    : AbstractValidator<GetAppointmentsFilterDoctorQuery>
{
    public GetAppointmentsFilterDoctorQueryHandlerValidator()
    {
        RuleFor(v => v.GetAppointmentsFilterDoctorRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must not exceed 200 characters.");

        RuleFor(v => v.GetAppointmentsFilterDoctorRequest.DataGridRequest.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to 0.");

        RuleFor(v => v.GetAppointmentsFilterDoctorRequest.DataGridRequest.Take)
            .GreaterThan(0).WithMessage("Take must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Take must be less than or equal to 100.");
    }
}