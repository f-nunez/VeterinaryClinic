using System.Text.Json;
using Contracts.Scheduling;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Factories;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest;

public class NotificationRequestService : INotificationRequestService
{
    private readonly IServiceBus _serviceBus;

    public NotificationRequestService(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task CreateAndSendAsync(
        INotificationRequestFactory factory,
        CancellationToken cancellationToken)
    {
        var notificationRequest = factory.CreateNotificationRequest();

        var message = new NotificationRequestSchedulingContract
        {
            CausationId = notificationRequest.CorrelationId,
            CorrelationId = notificationRequest.CorrelationId,
            Id = notificationRequest.CorrelationId,
            NotificationEvent = factory.GetNotificationEvent(),
            OccurredOn = DateTimeOffset.UtcNow,
            SerializedNotificationRequest = JsonSerializer.Serialize((object)notificationRequest)
        };

        await _serviceBus.PublishAsync(message, cancellationToken);
    }
}