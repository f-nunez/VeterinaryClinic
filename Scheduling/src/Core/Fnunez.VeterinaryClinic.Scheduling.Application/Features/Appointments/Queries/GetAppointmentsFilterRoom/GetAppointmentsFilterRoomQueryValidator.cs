using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterRoom;

public class GetAppointmentsFilterRoomQueryValidator
    : AbstractValidator<GetAppointmentsFilterRoomQuery>
{
    public GetAppointmentsFilterRoomQueryValidator()
    {
        RuleFor(v => v.GetAppointmentsFilterRoomRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must be less or equals than 200 characters.");
    }
}