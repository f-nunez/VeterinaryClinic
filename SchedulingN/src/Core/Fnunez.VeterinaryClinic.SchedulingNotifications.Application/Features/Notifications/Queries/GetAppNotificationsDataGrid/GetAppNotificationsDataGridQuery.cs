using MediatR;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries.GetAppNotificationsDataGrid;

public record GetAppNotificationsDataGridQuery(GetAppNotificationsDataGridRequest GetAppNotificationsDataGridRequest)
    : IRequest<GetAppNotificationsDataGridResponse>;