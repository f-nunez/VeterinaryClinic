using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Factories;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest;

public interface INotificationRequestService
{
    Task SendAsync(INotificationRequestFactory factory, CancellationToken cancellationToken);
}