using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetAppNotifications;

public record GetAppNotificationsQuery(GetAppNotificationsRequest GetNotificationsRequest)
    : IRequest<GetAppNotificationsResponse>;