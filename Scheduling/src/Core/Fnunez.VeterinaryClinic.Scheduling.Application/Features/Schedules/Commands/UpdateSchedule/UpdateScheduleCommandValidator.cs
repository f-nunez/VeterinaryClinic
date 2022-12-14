using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Commands.UpdateSchedule;

public class UpdateScheduleCommandValidator : AbstractValidator<UpdateScheduleCommand>
{
    public UpdateScheduleCommandValidator()
    {
        RuleFor(v => v.UpdateScheduleRequest.ClinicId)
            .GreaterThan(0).WithMessage("ClinicId is required.");

        RuleFor(v => v.UpdateScheduleRequest.EndOn)
            .NotNull().WithMessage("EndOn is required.");

        RuleFor(v => v.UpdateScheduleRequest.ScheduleId)
            .NotEmpty().WithMessage("ScheduleId is required.");

        RuleFor(v => v.UpdateScheduleRequest.StartOn)
            .NotNull().WithMessage("StartOn is required.");
    }
}