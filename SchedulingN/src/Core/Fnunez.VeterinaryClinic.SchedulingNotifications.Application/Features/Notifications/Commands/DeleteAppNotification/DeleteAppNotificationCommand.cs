using MediatR;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotification;

public record DeleteAppNotificationCommand(DeleteAppNotificationRequest DeleteAppNotificationRequest)
    : IRequest<DeleteAppNotificationResponse>;