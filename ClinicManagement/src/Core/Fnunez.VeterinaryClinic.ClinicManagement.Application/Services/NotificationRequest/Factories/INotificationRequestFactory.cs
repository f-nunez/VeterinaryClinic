using Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Services.NotificationRequest.Factories;

public interface INotificationRequestFactory
{
    BaseNotificationRequest CreateNotificationRequest();
    string GetNotificationEvent();
}