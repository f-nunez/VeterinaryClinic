using Fnunez.VeterinaryClinic.Public.Web.Helpers.SymmetricEncryption;
using Fnunez.VeterinaryClinic.Public.Web.ServiceBus;
using Fnunez.VeterinaryClinic.Public.Web.Services.Appointment;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.Public.Web.UnitTests.Services.Appointment;

public class AppointmentServiceTests
{
    [Fact]
    public async Task ConfirmAppointmentAsync_CallsPublishAsyncMethodOnceFromServiceBus()
    {
        // Arrange
        var mockILogger = new Mock<ILogger<AppointmentService>>();

        var mockIServiceBus = new Mock<IServiceBus>();

        mockIServiceBus.Setup(x =>
            x.PublishAsync(
                It.IsAny<object>(),
                CancellationToken.None
            )
        );

        var mockISymmetricEncryptionHelper = new Mock<ISymmetricEncryptionHelper>();

        mockISymmetricEncryptionHelper.Setup(x =>
            x.DecryptFromBase64Async(It.IsAny<string>())
        ).Returns(Task.FromResult(Guid.NewGuid().ToString()));

        var appointmentService = new AppointmentService
        (
            mockILogger.Object,
            mockIServiceBus.Object,
            mockISymmetricEncryptionHelper.Object
        );

        // Act
        await appointmentService.ConfirmAppointmentAsync("encryptedId");

        // Assert
        mockIServiceBus.Verify(x =>
            x.PublishAsync(
                It.IsAny<object>(),
                CancellationToken.None
            ),
            Times.Once()
        );
    }
}