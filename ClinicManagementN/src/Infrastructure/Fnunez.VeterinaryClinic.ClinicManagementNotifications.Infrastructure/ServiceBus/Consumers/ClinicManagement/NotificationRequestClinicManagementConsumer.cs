using Contracts.ClinicManagement;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services;
using MassTransit;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.ServiceBus.Consumers;

public class NotificationRequestClinicManagementConsumer
    : IConsumer<NotificationRequestClinicManagementContract>
{
    private readonly INotificationEngineService _notificationEngineService;

    public NotificationRequestClinicManagementConsumer(
        INotificationEngineService notificationEngineService)
    {
        _notificationEngineService = notificationEngineService;
    }

    public async Task Consume(
        ConsumeContext<NotificationRequestClinicManagementContract> context)
    {
        await _notificationEngineService.CreateAndNotifyAsync(
            context.Message.NotificationEvent,
            context.Message.SerializedNotificationRequest,
            context.CancellationToken
        );
    }
}