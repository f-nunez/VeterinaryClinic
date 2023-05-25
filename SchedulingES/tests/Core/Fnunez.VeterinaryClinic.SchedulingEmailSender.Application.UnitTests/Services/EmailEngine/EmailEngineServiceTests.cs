using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.EmailCompositions;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Requests;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate.Enums;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using Microsoft.Extensions.Logging;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.UnitTests.Services.EmailEngine;

public class EmailEngineServiceTests
{
    [Fact]
    public async Task CreateAndSendAsync_EmailEventStringHasNoMatchedValue_ThrowsArgumentException()
    {
        // Arrange
        var emailEventString = "AnyValue";

        var expectedErrorMessage = $"{nameof(emailEventString)} not found with value: {emailEventString}";

        var serializedEmailRequest = "serializedEmailRequest";

        var mockIEmailCompositionFactory = new Mock<IEmailCompositionFactory>();

        var mockIEmailRequestFactory = new Mock<IEmailRequestFactory>();

        var mockILogger = new Mock<ILogger<EmailEngineService>>();

        var mockIPayloadFactory = new Mock<IPayloadFactory>();

        var mockIServiceBus = new Mock<IServiceBus>();

        var mockIUnitOfWork = new Mock<IUnitOfWork>();

        var emailEngineService = new EmailEngineService(
            mockIEmailCompositionFactory.Object,
            mockIEmailRequestFactory.Object,
            mockILogger.Object,
            mockIPayloadFactory.Object,
            mockIServiceBus.Object,
            mockIUnitOfWork.Object
        );

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() =>
            emailEngineService.CreateAndSendAsync(
                emailEventString,
                serializedEmailRequest,
                CancellationToken.None
            )
        );

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task CreateAndSendAsync_EmailEventStringIsEmpty_ThrowsArgumentException(
        string emailEventString)
    {
        // Arrange
        var expectedErrorMessage = $"{nameof(emailEventString)} is empty.";

        var serializedEmailRequest = "serializedEmailRequest";

        var mockIEmailCompositionFactory = new Mock<IEmailCompositionFactory>();

        var mockIEmailRequestFactory = new Mock<IEmailRequestFactory>();

        var mockILogger = new Mock<ILogger<EmailEngineService>>();

        var mockIPayloadFactory = new Mock<IPayloadFactory>();

        var mockIServiceBus = new Mock<IServiceBus>();

        var mockIUnitOfWork = new Mock<IUnitOfWork>();

        var emailEngineService = new EmailEngineService(
            mockIEmailCompositionFactory.Object,
            mockIEmailRequestFactory.Object,
            mockILogger.Object,
            mockIPayloadFactory.Object,
            mockIServiceBus.Object,
            mockIUnitOfWork.Object
        );

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() =>
            emailEngineService.CreateAndSendAsync(
                emailEventString,
                serializedEmailRequest,
                CancellationToken.None
            )
        );

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task CreateAndSendAsync_SerializedEmailRequestIsEmpty_ThrowsArgumentException(
        string serializedEmailRequest)
    {
        // Arrange
        var expectedErrorMessage = $"{nameof(serializedEmailRequest)} is empty.";

        var emailEventString = EmailEvent.AppointmentConfirmed.ToString();

        var mockIEmailCompositionFactory = new Mock<IEmailCompositionFactory>();

        var mockIEmailRequestFactory = new Mock<IEmailRequestFactory>();

        var mockILogger = new Mock<ILogger<EmailEngineService>>();

        var mockIPayloadFactory = new Mock<IPayloadFactory>();

        var mockIServiceBus = new Mock<IServiceBus>();

        var mockIUnitOfWork = new Mock<IUnitOfWork>();

        var emailEngineService = new EmailEngineService(
            mockIEmailCompositionFactory.Object,
            mockIEmailRequestFactory.Object,
            mockILogger.Object,
            mockIPayloadFactory.Object,
            mockIServiceBus.Object,
            mockIUnitOfWork.Object
        );

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() =>
            emailEngineService.CreateAndSendAsync(
                emailEventString,
                serializedEmailRequest,
                CancellationToken.None
            )
        );

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }
}