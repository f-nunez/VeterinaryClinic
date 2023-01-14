using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRoomsFilterId;

public class GetRoomsFilterIdQueryValidator
    : AbstractValidator<GetRoomsFilterIdQuery>
{
    public GetRoomsFilterIdQueryValidator()
    {
        RuleFor(v => v.GetRoomsFilterIdRequest.IdFilterValue)
            .NotNull().WithMessage("IdFilterValue is required.")
            .NotEmpty().WithMessage("IdFilterValue is required.");
    }
}