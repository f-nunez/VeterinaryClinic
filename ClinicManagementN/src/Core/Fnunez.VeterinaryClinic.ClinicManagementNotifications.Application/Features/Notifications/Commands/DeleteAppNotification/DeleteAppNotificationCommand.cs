using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAppNotification;

public record DeleteAppNotificationCommand(DeleteAppNotificationRequest DeleteAppNotificationRequest)
    : IRequest<DeleteAppNotificationResponse>;