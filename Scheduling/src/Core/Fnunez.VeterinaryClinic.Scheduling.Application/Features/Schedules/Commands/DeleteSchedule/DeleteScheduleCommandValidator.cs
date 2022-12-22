using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Commands.DeleteSchedule;

public class DeleteScheduleCommandValidator
    : AbstractValidator<DeleteScheduleCommand>
{
    public DeleteScheduleCommandValidator()
    {
        RuleFor(v => v.DeleteScheduleRequest.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}