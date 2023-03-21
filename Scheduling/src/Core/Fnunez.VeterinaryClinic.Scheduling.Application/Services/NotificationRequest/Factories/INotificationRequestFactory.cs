using Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Requests;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.NotificationRequest.Factories;

public interface INotificationRequestFactory
{
    BaseNotificationRequest CreateNotificationRequest();
    string GetNotificationEvent();
}