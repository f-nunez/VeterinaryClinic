using System.Text.Json;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Requests;

public class EmailRequestFactory : IEmailRequestFactory
{
    public BaseEmailRequest GetEmailRequest(
        EmailEvent emailEvent,
        string serializedEmailRequest)
    {
        BaseEmailRequest? emailRequest;

        switch (emailEvent)
        {
            case EmailEvent.AppointmentConfirmed:
                emailRequest = JsonSerializer.Deserialize<AppointmentConfirmedEmailRequest>(serializedEmailRequest);
                break;
            case EmailEvent.AppointmentCreated:
                emailRequest = JsonSerializer.Deserialize<AppointmentCreatedEmailRequest>(serializedEmailRequest);
                break;
            case EmailEvent.AppointmentDeleted:
                emailRequest = JsonSerializer.Deserialize<AppointmentDeletedEmailRequest>(serializedEmailRequest);
                break;
            case EmailEvent.AppointmentUpdated:
                emailRequest = JsonSerializer.Deserialize<AppointmentUpdatedEmailRequest>(serializedEmailRequest);
                break;
            default:
                throw new ArgumentException(
                    $"{nameof(emailEvent)} not found with value: {emailEvent}");
        }

        if (emailRequest is null)
            throw new ArgumentNullException(nameof(emailRequest));

        return emailRequest;
    }
}