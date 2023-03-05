using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetAppNotifications;

public class GetAppNotificationsQueryHandler
    : IRequestHandler<GetAppNotificationsQuery, GetAppNotificationsResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public GetAppNotificationsQueryHandler(
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppNotificationsResponse> Handle(
        GetAppNotificationsQuery query,
        CancellationToken cancellationToken)
    {
        var request = query.GetNotificationsRequest;
        var response = new GetAppNotificationsResponse(request.CorrelationId);

        var specification = new AppNotificationSpecification(
            request, _currentUserService.UserId);

        var appNotifications = await _unitOfWork
            .ReadRepository<AppNotification>()
            .ListAsync(specification, cancellationToken);

        var appNotificationsCount = await _unitOfWork
            .ReadRepository<AppNotification>()
            .CountAsync(specification, cancellationToken);

        var appNotificationDtos = new List<AppNotificationDto>();

        foreach (var appNotification in appNotifications)
        {
            appNotificationDtos.Add(
                new AppNotificationDto
                {
                    CreatedOn = appNotification.CreatedOn,
                    Event = appNotification.Notification.NotificationEvent.ToString(),
                    Id = appNotification.Id,
                    IsRead = appNotification.ReadOn.HasValue,
                    Payload = appNotification.Notification.Payload,
                    TriggeredBy = appNotification.Notification.TriggeredByUser.Name
                }
            );
        }

        response.AppNotifications = appNotificationDtos;
        response.Count = appNotificationsCount;

        return response;
    }
}