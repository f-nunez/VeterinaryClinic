using FluentValidation;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.MarkAppNotificationAsRead;

public class MarkAppNotificationAsReadCommandValidator
    : AbstractValidator<MarkAppNotificationAsReadCommand>
{
    public MarkAppNotificationAsReadCommandValidator()
    {
        RuleFor(v => v.MarkNotificationAsReadRequest.AppNotificationId)
            .NotNull().WithMessage("AppNotificationId is required.")
            .NotEmpty().WithMessage("AppNotificationId is required.");
    }
}