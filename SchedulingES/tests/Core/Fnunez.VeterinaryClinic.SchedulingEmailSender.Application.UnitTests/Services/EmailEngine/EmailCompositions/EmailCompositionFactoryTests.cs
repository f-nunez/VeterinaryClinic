using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.EmailCompositions;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.Language;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.StringRazorRender;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;
namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.UnitTests.Services.EmailEngine.EmailCompositions;

public class EmailCompositionFactoryTests
{
    [Fact]
    public async Task GetEmailCompositionAsync_AllEmailEventsAreFound_IsValid()
    {
        // Arrange
        BasePayload? basePayload = null;

        var allEmailEvents = Enum.GetValues(typeof(EmailEvent))
            .Cast<EmailEvent>();

        var allEmailEventsAreFound = true;

        var mockILanguageService = new Mock<ILanguageService>();

        var mockIStringRazorRenderService = new Mock<IStringRazorRenderService>();

        // Act
        foreach (var emailEvent in allEmailEvents)
        {
            try
            {
                var factory = new EmailCompositionFactory(
                    mockILanguageService.Object, mockIStringRazorRenderService.Object);

                await factory.GetEmailCompositionAsync(emailEvent, basePayload!);
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
    public async Task GetEmailCompositionAsync_EmailEventIsAppointmentConfirmed_ReturnsEmailComposition()
    {
        // Arrange
        var emailEvent = EmailEvent.AppointmentConfirmed;

        var emailBody = "html for email body";

        var emailSubject = "translation for subject email";

        var emailTo = "test@nunez.ninja";

        var payload = new AppointmentConfirmedPayload { SendTo = emailTo };

        var mockILanguageService = new Mock<ILanguageService>();

        mockILanguageService.Setup(x => x.GetStringLocalizer(
            It.IsAny<string>(), It.IsAny<string>()
        )).Returns(emailSubject);

        var mockIStringRazorRenderService = new Mock<IStringRazorRenderService>();

        mockIStringRazorRenderService.Setup(x => x.RenderRazorToStringAsync(
            It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()
        )).Returns(Task.FromResult(emailBody));

        var factory = new EmailCompositionFactory(
            mockILanguageService.Object, mockIStringRazorRenderService.Object);

        // Act
        var actual = await factory.GetEmailCompositionAsync(emailEvent, payload);

        // Assert
        Assert.IsType<EmailComposition>(actual);

        Assert.Equal(emailBody, actual.Body);

        Assert.True(actual.IsBodyHtml);

        Assert.Equal(emailSubject, actual.Subject);

        Assert.Equal(emailTo, actual.To);
    }

    [Fact]
    public async Task GetEmailCompositionAsync_EmailEventIsAppointmentCreated_ReturnsEmailComposition()
    {
        // Arrange
        var emailEvent = EmailEvent.AppointmentCreated;

        var emailBody = "html for email body";

        var emailSubject = "translation for subject email";

        var emailTo = "test@nunez.ninja";

        var payload = new AppointmentCreatedPayload { SendTo = emailTo };

        var mockILanguageService = new Mock<ILanguageService>();

        mockILanguageService.Setup(x => x.GetStringLocalizer(
            It.IsAny<string>(), It.IsAny<string>()
        )).Returns(emailSubject);

        var mockIStringRazorRenderService = new Mock<IStringRazorRenderService>();

        mockIStringRazorRenderService.Setup(x => x.RenderRazorToStringAsync(
            It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()
        )).Returns(Task.FromResult(emailBody));

        var factory = new EmailCompositionFactory(
            mockILanguageService.Object, mockIStringRazorRenderService.Object);

        // Act
        var actual = await factory.GetEmailCompositionAsync(emailEvent, payload);

        // Assert
        Assert.IsType<EmailComposition>(actual);

        Assert.Equal(emailBody, actual.Body);

        Assert.True(actual.IsBodyHtml);

        Assert.Equal(emailSubject, actual.Subject);

        Assert.Equal(emailTo, actual.To);
    }

    [Fact]
    public async Task GetEmailCompositionAsync_EmailEventIsAppointmentDeleted_ReturnsEmailComposition()
    {
        // Arrange
        var emailEvent = EmailEvent.AppointmentDeleted;

        var emailBody = "html for email body";

        var emailSubject = "translation for subject email";

        var emailTo = "test@nunez.ninja";

        var payload = new AppointmentDeletedPayload { SendTo = emailTo };

        var mockILanguageService = new Mock<ILanguageService>();

        mockILanguageService.Setup(x => x.GetStringLocalizer(
            It.IsAny<string>(), It.IsAny<string>()
        )).Returns(emailSubject);

        var mockIStringRazorRenderService = new Mock<IStringRazorRenderService>();

        mockIStringRazorRenderService.Setup(x => x.RenderRazorToStringAsync(
            It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()
        )).Returns(Task.FromResult(emailBody));

        var factory = new EmailCompositionFactory(
            mockILanguageService.Object, mockIStringRazorRenderService.Object);

        // Act
        var actual = await factory.GetEmailCompositionAsync(emailEvent, payload);

        // Assert
        Assert.IsType<EmailComposition>(actual);

        Assert.Equal(emailBody, actual.Body);

        Assert.True(actual.IsBodyHtml);

        Assert.Equal(emailSubject, actual.Subject);

        Assert.Equal(emailTo, actual.To);
    }

    [Fact]
    public async Task GetEmailCompositionAsync_EmailEventIsAppointmentUpdated_ReturnsEmailComposition()
    {
        // Arrange
        var emailEvent = EmailEvent.AppointmentUpdated;

        var emailBody = "html for email body";

        var emailSubject = "translation for subject email";

        var emailTo = "test@nunez.ninja";

        var payload = new AppointmentUpdatedPayload { SendTo = emailTo };

        var mockILanguageService = new Mock<ILanguageService>();

        mockILanguageService.Setup(x => x.GetStringLocalizer(
            It.IsAny<string>(), It.IsAny<string>()
        )).Returns(emailSubject);

        var mockIStringRazorRenderService = new Mock<IStringRazorRenderService>();

        mockIStringRazorRenderService.Setup(x => x.RenderRazorToStringAsync(
            It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()
        )).Returns(Task.FromResult(emailBody));

        var factory = new EmailCompositionFactory(
            mockILanguageService.Object, mockIStringRazorRenderService.Object);

        // Act
        var actual = await factory.GetEmailCompositionAsync(emailEvent, payload);

        // Assert
        Assert.IsType<EmailComposition>(actual);

        Assert.Equal(emailBody, actual.Body);

        Assert.True(actual.IsBodyHtml);

        Assert.Equal(emailSubject, actual.Subject);

        Assert.Equal(emailTo, actual.To);
    }
}