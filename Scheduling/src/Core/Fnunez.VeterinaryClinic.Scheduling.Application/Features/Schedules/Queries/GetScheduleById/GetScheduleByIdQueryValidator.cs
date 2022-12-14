using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Queries.GetScheduleById;

public class GetScheduleByIdQueryValidator : AbstractValidator<GetScheduleByIdQuery>
{
    public GetScheduleByIdQueryValidator()
    {
        RuleFor(v => v.GetScheduleByIdRequest.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}