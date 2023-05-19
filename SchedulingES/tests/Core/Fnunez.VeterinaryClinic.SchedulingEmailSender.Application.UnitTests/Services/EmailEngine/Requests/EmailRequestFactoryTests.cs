using System.Text.Json;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Requests;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.UnitTests.Services.EmailEngine.Requests;

public class EmailRequestFactoryTests
{
    [Fact]
    public void GetEmailRequest_AllEmailEventsAreFound_IsValid()
    {
        // Arrange
        string serializedEmailRequest = "serializedEmailRequest";

        var allEmailEvents = Enum.GetValues(typeof(EmailEvent))
            .Cast<EmailEvent>();

        var allEmailEventsAreFound = true;

        // Act
        foreach (var emailEvent in allEmailEvents)
        {
            try
            {
                var factory = new EmailRequestFactory();

                factory.GetEmailRequest(emailEvent, serializedEmailRequest!);
            }
            catch (Exception ex)
            {
                if (ex.Message == $"{nameof(emailEvent)} not found with value: {emailEvent}")
                {
                    allEmailEventsAreFound = false;
                    break;
                }
            }
        }

        // Assert
        Assert.True(allEmailEventsAreFound);
    }

    [Fact]
    public void GetEmailRequest_EmailEventIsAppointmentConfirmed_ReturnsAppointmentConfirmedEmailRequest()
    {
        // Arrange
        var emailEvent = EmailEvent.AppointmentConfirmed;

        var serializedEmailRequest = JsonSerializer.Serialize(
            new AppointmentConfirmedEmailRequest());

        var factory = new EmailRequestFactory();

        // Act
        var actual = factory.GetEmailRequest(emailEvent, serializedEmailRequest!);

        // Assert
        Assert.IsType<AppointmentConfirmedEmailRequest>(actual);
    }

    [Fact]
    public void GetEmailRequest_EmailEventIsAppointmentCreated_ReturnsAppointmentCreatedEmailRequest()
    {
        // Arrange
        var emailEvent = EmailEvent.AppointmentCreated;

        var serializedEmailRequest = JsonSerializer.Serialize(
            new AppointmentCreatedEmailRequest());

        var factory = new EmailRequestFactory();

        // Act
        var actual = factory.GetEmailRequest(emailEvent, serializedEmailRequest!);

        // Assert
        Assert.IsType<AppointmentCreatedEmailRequest>(actual);
    }

    [Fact]
    public void GetEmailRequest_EmailEventIsAppointmentDeleted_ReturnsAppointmentDeletedEmailRequest()
    {
        // Arrange
        var emailEvent = EmailEvent.AppointmentDeleted;

        var serializedEmailRequest = JsonSerializer.Serialize(
            new AppointmentDeletedEmailRequest());

        var factory = new EmailRequestFactory();

        // Act
        var actual = factory.GetEmailRequest(emailEvent, serializedEmailRequest!);

        // Assert
        Assert.IsType<AppointmentDeletedEmailRequest>(actual);
    }

    [Fact]
    public void GetEmailRequest_EmailEventIsAppointmentUpdated_ReturnsAppointmentUpdatedEmailRequest()
    {
        // Arrange
        var emailEvent = EmailEvent.AppointmentUpdated;

        var serializedEmailRequest = JsonSerializer.Serialize(
            new AppointmentUpdatedEmailRequest());

        var factory = new EmailRequestFactory();

        // Act
        var actual = factory.GetEmailRequest(emailEvent, serializedEmailRequest!);

        // Assert
        Assert.IsType<AppointmentUpdatedEmailRequest>(actual);
    }
}