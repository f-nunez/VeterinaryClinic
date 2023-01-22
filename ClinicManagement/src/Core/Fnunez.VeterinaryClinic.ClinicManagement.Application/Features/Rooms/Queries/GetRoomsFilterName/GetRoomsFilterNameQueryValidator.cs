using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRoomsFilterName;

public class GetRoomsFilterNameQueryValidator
    : AbstractValidator<GetRoomsFilterNameQuery>
{
    public GetRoomsFilterNameQueryValidator()
    {
        RuleFor(v => v.GetRoomsFilterNameRequest.NameFilterValue)
            .NotNull().WithMessage("NameFilterValue is required.")
            .NotEmpty().WithMessage("NameFilterValue is required.");
    }
}