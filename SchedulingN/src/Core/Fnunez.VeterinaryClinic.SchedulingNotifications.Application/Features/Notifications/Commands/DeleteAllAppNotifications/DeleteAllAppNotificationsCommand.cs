using MediatR;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAllAppNotifications;

public record DeleteAllAppNotificationsCommand(DeleteAllAppNotificationsRequest DeleteAllAppNotificationsRequest)
    : IRequest<DeleteAllAppNotificationsResponse>;