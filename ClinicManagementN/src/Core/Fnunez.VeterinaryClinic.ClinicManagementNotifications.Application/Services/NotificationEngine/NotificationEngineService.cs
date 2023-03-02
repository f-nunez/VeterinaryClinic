using System.Text.Json;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Specifications;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.ApplicationUserAggregate;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate.Enums;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services;

public class NotificationEngineService : INotificationEngineService
{
    private readonly INotificationRequestFactory _notificationRequestFactory;
    private readonly IPayloadFactory _payloadFactory;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationEngineService(
        INotificationRequestFactory notificationRequestFactory,
        IPayloadFactory payloadFactory,
        IUnitOfWork unitOfWork)
    {
        _notificationRequestFactory = notificationRequestFactory;
        _payloadFactory = payloadFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<AppNotification>> CreateAndNotifyAsync(
        string notificationEventString,
        string serializedNotificationRequest)
    {
        if (string.IsNullOrEmpty(notificationEventString))
            throw new ArgumentNullException(nameof(notificationEventString));

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
            notificationEvent, notificationRequest, payload);

        var appNotifications = await CreateAppNotificationsAsync(notification);

        var userIdsToNotify = appNotifications.Select(x => x.UserId).ToArray();

        //send asyncronous without awaiter appNotifications to message queue to be consumed by SignalR Hub

        return appNotifications;
    }

    private async Task<Notification> CreateNotificationAsync(
        NotificationEvent notificationEvent,
        BaseNotificationRequest notificationRequest,
        BasePayload payload)
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
            .AddAsync(notification);

        await _unitOfWork.CommitAsync();

        return notification;
    }

    private async Task<List<AppNotification>> CreateAppNotificationsAsync(Notification notification)
    {
        var specification = new UsersExceptWhoTriggerTheEventSpecification(
            notification.TriggeredByUserId);

        var users = await _unitOfWork
            .Repository<ApplicationUser>()
            .ListAsync(specification);

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
            .AddRangeAsync(appNotifications);

        await _unitOfWork.CommitAsync();

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