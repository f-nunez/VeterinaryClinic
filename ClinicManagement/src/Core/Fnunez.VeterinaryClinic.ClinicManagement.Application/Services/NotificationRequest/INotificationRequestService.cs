using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest;

public interface INotificationRequestService
{
    Task CreateAndSendAsync(INotificationRequestFactory factory, CancellationToken cancellationToken);
}