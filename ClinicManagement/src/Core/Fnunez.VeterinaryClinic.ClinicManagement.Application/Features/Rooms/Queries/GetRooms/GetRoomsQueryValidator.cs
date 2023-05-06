using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRooms;

public class GetRoomsQueryValidator : AbstractValidator<GetRoomsQuery>
{
    public GetRoomsQueryValidator()
    {
        RuleFor(v => v.GetRoomsRequest.DataGridRequest.Search)
           .MaximumLength(200).WithMessage("Search must not exceed 200 characters.");

        RuleFor(v => v.GetRoomsRequest.DataGridRequest.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to 0.");

        RuleFor(v => v.GetRoomsRequest.DataGridRequest.Take)
            .GreaterThanOrEqualTo(0).WithMessage("Take must be greater than or equal to 0.")
            .LessThanOrEqualTo(100).WithMessage("Take must be less than or equal to 100.");

        RuleFor(v => v.GetRoomsRequest.IdFilterValue)
            .MaximumLength(200).WithMessage("IdFilterValue must not exceed 200 characters.");

        RuleFor(v => v.GetRoomsRequest.NameFilterValue)
            .MaximumLength(200).WithMessage("NameFilterValue must not exceed 200 characters.");
    }
}