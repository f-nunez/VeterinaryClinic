using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Helpers.SymmetricEncryption;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Services.EmailTemplate;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.Settings;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Api.UnitTests.Services.EmailTemplate;

public class EmailTemplateServiceTests
{
    [Fact]
    public async Task GetConfirmationUrlAsync_ReturnsConfirmationUrl()
    {
        // Arrange
        var confirmationUrlFormat = "https://localhost:7138/appointment/confirm?id={0}&language={1}";

        var encryptedId = "AnyEncryptedId";

        var language = "English";

        var expectedConfirmationUrl = String.Format(
            confirmationUrlFormat, encryptedId, language);

        var mockIEmailTemplateSetting = new Mock<IEmailTemplateSetting>();

        mockIEmailTemplateSetting.Setup(x =>
            x.UrlQueryToConfirmAppointment
        ).Returns(confirmationUrlFormat);

        var mockISymmetricEncryptionHelper = new Mock<ISymmetricEncryptionHelper>();

        mockISymmetricEncryptionHelper.Setup(x =>
            x.EncryptToBase64Async(It.IsAny<string>())
        ).Returns(Task.FromResult(encryptedId));

        var emailTemplateService = new EmailTemplateService(
            mockIEmailTemplateSetting.Object,
            mockISymmetricEncryptionHelper.Object
        );

        // Act
        var actual = await emailTemplateService
            .GetConfirmationUrlAsync(Guid.NewGuid(), language);

        // Assert
        Assert.Equal(expectedConfirmationUrl, actual);
    }
}