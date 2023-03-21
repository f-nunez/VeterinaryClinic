using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Factories;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest;

public interface INotificationRequestService
{
    Task CreateAndSendAsync(INotificationRequestFactory factory, CancellationToken cancellationToken);
}