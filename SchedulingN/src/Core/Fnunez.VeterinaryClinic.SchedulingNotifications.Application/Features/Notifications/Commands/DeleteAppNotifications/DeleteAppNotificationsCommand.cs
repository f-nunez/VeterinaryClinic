using MediatR;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotifications;

public record DeleteAppNotificationsCommand(DeleteAppNotificationsRequest DeleteAppNotificationsRequest)
    : IRequest<DeleteAppNotificationsResponse>;