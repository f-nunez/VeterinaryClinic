using Fnunez.VeterinaryClinic.EmailService.Api.Services.Email;
using Fnunez.VeterinaryClinic.EmailService.Api.Settings;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.EmailService.UnitTests.Api.Services.Email;

public class EmailServiceTests
{
    private readonly string _body = "html";
    private readonly bool _isBodyHtml = true;
    private readonly string _subject = "subject";
    private readonly string _to = "test@nunez.ninja";

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void SendEmail_EmailCompositionBodyIsEmpty_ThrowsArgumentException(
        string emailCompositionBody)
    {
        // Arrange
        var emailComposition = new EmailComposition
        {
            Body = emailCompositionBody,
            IsBodyHtml = _isBodyHtml,
            Subject = _subject,
            To = _to
        };

        var expectedErrorMessage = $"{nameof(emailComposition.Body)} is empty.";

        var mockIEmailSetting = new Mock<IEmailSetting>();

        var mockILogger = new Mock<ILogger<Fnunez.VeterinaryClinic.EmailService.Api.Services.Email.EmailService>>();

        var emailService = new Fnunez.VeterinaryClinic.EmailService.Api.Services.Email.EmailService(
            mockIEmailSetting.Object, mockILogger.Object);

        // Act
        var actual = Assert.Throws<ArgumentException>(() =>
            emailService.SendEmail(emailComposition));

        // Assert
        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void SendEmail_EmailCompositionSubjectIsEmpty_ThrowsArgumentException(
        string emailCompositionSubject)
    {
        // Arrange
        var emailComposition = new EmailComposition
        {
            Body = _body,
            IsBodyHtml = _isBodyHtml,
            Subject = emailCompositionSubject,
            To = _to
        };

        var expectedErrorMessage = $"{nameof(emailComposition.Subject)} is empty.";

        var mockIEmailSetting = new Mock<IEmailSetting>();

        var mockILogger = new Mock<ILogger<Fnunez.VeterinaryClinic.EmailService.Api.Services.Email.EmailService>>();

        var emailService = new Fnunez.VeterinaryClinic.EmailService.Api.Services.Email.EmailService(
            mockIEmailSetting.Object, mockILogger.Object);

        // Act
        var actual = Assert.Throws<ArgumentException>(() =>
            emailService.SendEmail(emailComposition));

        // Assert
        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void SendEmail_EmailCompositionToIsEmpty_ThrowsArgumentException(
        string emailCompositionTo)
    {
        // Arrange
        var emailComposition = new EmailComposition
        {
            Body = _body,
            IsBodyHtml = _isBodyHtml,
            Subject = _subject,
            To = emailCompositionTo
        };

        var expectedErrorMessage = $"{nameof(emailComposition.To)} is empty.";

        var mockIEmailSetting = new Mock<IEmailSetting>();

        var mockILogger = new Mock<ILogger<Fnunez.VeterinaryClinic.EmailService.Api.Services.Email.EmailService>>();

        var emailService = new Fnunez.VeterinaryClinic.EmailService.Api.Services.Email.EmailService(
            mockIEmailSetting.Object, mockILogger.Object);

        // Act
        var actual = Assert.Throws<ArgumentException>(() =>
            emailService.SendEmail(emailComposition));

        // Assert
        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }
}