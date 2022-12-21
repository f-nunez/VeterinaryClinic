using FluentValidation;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorsFilterId;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomsFilterId;

public class GetDoctorsFilterIdQueryValidator
    : AbstractValidator<GetDoctorsFilterIdQuery>
{
    public GetDoctorsFilterIdQueryValidator()
    {
        RuleFor(v => v.GetDoctorsFilterIdRequest.IdFilterValue)
            .NotNull().WithMessage("IdFilterValue is required.")
            .NotEmpty().WithMessage("IdFilterValue is required.");
    }
}