using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;

public interface INotificationRequestService
{
    Task SendAsync(INotificationRequestFactory factory, CancellationToken cancellationToken);
}