using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Commands.CreateSchedule;

public class CreateScheduleCommandValidator
    : AbstractValidator<CreateScheduleCommand>
{
    public CreateScheduleCommandValidator()
    {
        RuleFor(v => v.CreateScheduleRequest.ClinicId)
            .GreaterThan(0).WithMessage("ClinicId is required.");

        RuleFor(v => v.CreateScheduleRequest.EndOn)
            .NotNull().WithMessage("EndOn is required.");

        RuleFor(v => v.CreateScheduleRequest.StartOn)
            .NotNull().WithMessage("StartOn is required.");
    }
}