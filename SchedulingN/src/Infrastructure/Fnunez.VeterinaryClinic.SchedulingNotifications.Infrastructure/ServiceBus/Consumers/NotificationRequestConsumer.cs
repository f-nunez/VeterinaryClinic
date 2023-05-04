using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services;
using MassTransit;
using SchedulingContracts;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.ServiceBus.Consumers;

public class NotificationRequestConsumer
    : IConsumer<NotificationRequestContract>
{
    private readonly INotificationEngineService _notificationEngineService;

    public NotificationRequestConsumer(
        INotificationEngineService notificationEngineService)
    {
        _notificationEngineService = notificationEngineService;
    }

    public async Task Consume(
        ConsumeContext<NotificationRequestContract> context)
    {
        await _notificationEngineService.CreateAndNotifyAsync(
            context.Message.NotificationEvent,
            context.Message.SerializedNotificationRequest,
            context.CancellationToken
        );
    }
}