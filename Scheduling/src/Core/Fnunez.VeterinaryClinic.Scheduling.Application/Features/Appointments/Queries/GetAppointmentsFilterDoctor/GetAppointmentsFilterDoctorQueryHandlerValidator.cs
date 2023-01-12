using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterDoctor;

public class GetAppointmentsFilterDoctorQueryHandlerValidator
    : AbstractValidator<GetAppointmentsFilterDoctorQuery>
{
    public GetAppointmentsFilterDoctorQueryHandlerValidator()
    {
        RuleFor(v => v.GetAppointmentsFilterDoctorRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must be less or equals than 200 characters.");
    }
}