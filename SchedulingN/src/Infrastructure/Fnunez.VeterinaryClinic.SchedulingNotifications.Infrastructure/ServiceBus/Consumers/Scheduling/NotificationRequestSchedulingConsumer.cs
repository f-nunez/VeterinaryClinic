using Contracts.Scheduling;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services;
using MassTransit;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.ServiceBus.Consumers;

public class NotificationRequestSchedulingConsumer
    : IConsumer<NotificationRequestSchedulingContract>
{
    private readonly INotificationEngineService _notificationEngineService;

    public NotificationRequestSchedulingConsumer(
        INotificationEngineService notificationEngineService)
    {
        _notificationEngineService = notificationEngineService;
    }

    public async Task Consume(
        ConsumeContext<NotificationRequestSchedulingContract> context)
    {
        await _notificationEngineService.CreateAndNotifyAsync(
            context.Message.NotificationEvent,
            context.Message.SerializedNotificationRequest,
            context.CancellationToken
        );
    }
}