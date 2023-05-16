using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryValidator : AbstractValidator<GetRoomByIdQuery>
{
    public GetRoomByIdQueryValidator()
    {
        RuleFor(v => v.GetRoomByIdRequest.Id)
            .GreaterThan(0).WithMessage("Id is required.");
    }
}