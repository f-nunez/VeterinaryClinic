using MediatR;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries.GetAppNotifications;

public record GetAppNotificationsQuery(GetAppNotificationsRequest GetNotificationsRequest)
    : IRequest<GetAppNotificationsResponse>;