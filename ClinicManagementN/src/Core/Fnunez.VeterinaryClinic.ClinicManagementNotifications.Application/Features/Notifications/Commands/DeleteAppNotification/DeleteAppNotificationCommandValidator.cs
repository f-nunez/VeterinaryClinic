using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAppNotification;

public class DeleteAppNotificationCommandValidator : AbstractValidator<DeleteAppNotificationCommand>
{
    public DeleteAppNotificationCommandValidator()
    {
        RuleFor(v => v.DeleteAppNotificationRequest.AppNotificationId)
            .NotEmpty().WithMessage("AppNotificationId is required.");
    }
}