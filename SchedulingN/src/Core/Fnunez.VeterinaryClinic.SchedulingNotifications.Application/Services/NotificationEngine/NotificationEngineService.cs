using System.Text.Json;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Specifications;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationHub;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.ApplicationUserAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate.Enums;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services;

public class NotificationEngineService : INotificationEngineService
{
    private readonly INotificationHubService _notificationHubService;
    private readonly INotificationRequestFactory _notificationRequestFactory;
    private readonly IPayloadFactory _payloadFactory;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationEngineService(
        INotificationHubService notificationHubService,
        INotificationRequestFactory notificationRequestFactory,
        IPayloadFactory payloadFactory,
        IUnitOfWork unitOfWork)
    {
        _notificationHubService = notificationHubService;
        _notificationRequestFactory = notificationRequestFactory;
        _payloadFactory = payloadFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<AppNotification>> CreateAndNotifyAsync(
        string notificationEventString,
        string serializedNotificationRequest,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(notificationEventString))
            throw new ArgumentException(
                $"{nameof(notificationEventString)} is empty.");

        if (string.IsNullOrEmpty(serializedNotificationRequest))
            throw new ArgumentNullException(
                nameof(serializedNotificationRequest));

        NotificationEvent notificationEvent = GetNotificationEvent(
            notificationEventString);

        BaseNotificationRequest notificationRequest = _notificationRequestFactory
            .GetNotificationRequest(notificationEvent, serializedNotificationRequest);

        BasePayload payload = _payloadFactory
            .GetPayload(notificationEvent, notificationRequest);

        var notification = await CreateNotificationAsync(
            notificationEvent, notificationRequest, payload, cancellationToken);

        var appNotifications = await CreateAppNotificationsAsync(
            notification, cancellationToken);

        foreach (var appNotification in appNotifications)
            await _notificationHubService.SendAppNotificationAsync(
                appNotification.UserId, $"{appNotification.Id}");

        return appNotifications;
    }

    private async Task<Notification> CreateNotificationAsync(
        NotificationEvent notificationEvent,
        BaseNotificationRequest notificationRequest,
        BasePayload payload,
        CancellationToken cancellationToken)
    {
        var notification = new Notification(
            notificationRequest.CorrelationId,
            DateTimeOffset.UtcNow,
            notificationEvent,
            JsonSerializer.Serialize((object)payload),
            notificationRequest.TriggeredByUserId
        );

        await _unitOfWork
            .Repository<Notification>()
            .AddAsync(notification, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return notification;
    }

    private async Task<List<AppNotification>> CreateAppNotificationsAsync(
        Notification notification,
        CancellationToken cancellationToken)
    {
        var specification = new UsersExceptWhoTriggerTheEventSpecification(
            notification.TriggeredByUserId);

        var users = await _unitOfWork
            .Repository<ApplicationUser>()
            .ListAsync(specification, cancellationToken);

        var appNotifications = new List<AppNotification>();

        foreach (var user in users)
        {
            appNotifications.Add(
                new AppNotification(
                    Guid.NewGuid(),
                    notification.CreatedOn,
                    notification.Id,
                    user.Id
                )
            );
        }

        await _unitOfWork
            .Repository<AppNotification>()
            .AddRangeAsync(appNotifications, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return appNotifications;
    }

    private NotificationEvent GetNotificationEvent(
        string notificationEventString)
    {
        bool isParsedNotificationEvent = Enum.TryParse(
            notificationEventString, out NotificationEvent notificationEvent);

        if (isParsedNotificationEvent)
            return notificationEvent;

        throw new ArgumentException(
            $"{nameof(notificationEventString)} not found with value: {notificationEventString}");
    }
}