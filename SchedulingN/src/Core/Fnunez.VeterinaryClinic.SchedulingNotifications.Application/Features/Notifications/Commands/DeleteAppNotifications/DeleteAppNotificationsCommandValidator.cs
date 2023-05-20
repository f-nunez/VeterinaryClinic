using FluentValidation;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotifications;

public class DeleteAppNotificationsCommandValidator
    : AbstractValidator<DeleteAppNotificationsCommand>
{
    public DeleteAppNotificationsCommandValidator()
    {
        RuleFor(v => v.DeleteAppNotificationsRequest.AppNotificationIds)
            .NotNull().WithMessage("AppNotificationIds is required.")
            .NotEmpty().WithMessage("AppNotificationIds is required.");
    }
}