using MediatR;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.MarkAppNotificationAsRead;

public record MarkAppNotificationAsReadCommand(MarkAppNotificationAsReadRequest MarkNotificationAsReadRequest)
    : IRequest<MarkAppNotificationAsReadResponse>;