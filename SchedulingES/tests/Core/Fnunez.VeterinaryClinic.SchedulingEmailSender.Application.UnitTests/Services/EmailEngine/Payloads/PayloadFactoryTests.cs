using AutoMapper;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Mappings;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Requests;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.UnitTests.Services.EmailEngine.Payloads;

public class PayloadFactoryTests
{
    [Fact]
    public void GetPayload_AllEmailEventsAreFound_IsValid()
    {
        // Arrange
        BaseEmailRequest? emailRequest = null;

        var allEmailEvents = Enum.GetValues(typeof(EmailEvent))
            .Cast<EmailEvent>();

        var allEmailEventsAreFound = true;

        var mockIMapper = new Mock<IMapper>();

        // Act
        foreach (var emailEvent in allEmailEvents)
        {
            try
            {
                var factory = new PayloadFactory(mockIMapper.Object);

                factory.GetPayload(emailEvent, emailRequest!);
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
    public void GetPayload_EmailEventIsAppointmentConfirmed_ReturnsAppointmentConfirmedPayload()
    {
        // Arrange
        var emailEvent = EmailEvent.AppointmentConfirmed;

        var emailRequest = new AppointmentConfirmedEmailRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new AppointmentProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(emailEvent, emailRequest);

        // Assert
        Assert.IsType<AppointmentConfirmedPayload>(actual);
    }

    [Fact]
    public void GetPayload_EmailEventIsAppointmentCreated_ReturnsAppointmentCreatedPayload()
    {
        // Arrange
        var emailEvent = EmailEvent.AppointmentCreated;

        var emailRequest = new AppointmentCreatedEmailRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new AppointmentProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(emailEvent, emailRequest);

        // Assert
        Assert.IsType<AppointmentCreatedPayload>(actual);
    }

    [Fact]
    public void GetPayload_EmailEventIsAppointmentDeleted_ReturnsAppointmentDeletedPayload()
    {
        // Arrange
        var emailEvent = EmailEvent.AppointmentDeleted;

        var emailRequest = new AppointmentDeletedEmailRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new AppointmentProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(emailEvent, emailRequest);

        // Assert
        Assert.IsType<AppointmentDeletedPayload>(actual);
    }

    [Fact]
    public void GetPayload_EmailEventIsAppointmentUpdated_ReturnsAppointmentUpdatedPayload()
    {
        // Arrange
        var emailEvent = EmailEvent.AppointmentUpdated;

        var emailRequest = new AppointmentUpdatedEmailRequest();

        var mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new AppointmentProfile())));

        var factory = new PayloadFactory(mapper);

        // Act
        var actual = factory.GetPayload(emailEvent, emailRequest);

        // Assert
        Assert.IsType<AppointmentUpdatedPayload>(actual);
    }
}