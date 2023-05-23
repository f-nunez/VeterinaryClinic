using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.Factories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Services.IntegrationEventReceiver;

public class IntegrationEventReceiverServiceTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task ReceiveAsync_IntegrationEventStringIsEmpty_ThrowsArgumentException(
        string integrationEventString)
    {
        // Arrange
        var mockIIntegrationEventFactory = new Mock<IIntegrationEventFactory>();

        var mockIMediator = new Mock<IMediator>();

        var integrationEventReceiverService = new IntegrationEventReceiverService
        (
            mockIIntegrationEventFactory.Object,
            mockIMediator.Object
        );

        var expectedErrorMessage = $"{nameof(integrationEventString)} is empty.";

        var serializedIntegrationEvent = "serializedIntegrationEvent";

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() =>
            integrationEventReceiverService.ReceiveAsync(
                integrationEventString,
                serializedIntegrationEvent,
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
    public async Task ReceiveAsync_SerializedIntegrationEventIsEmpty_ThrowsArgumentException(
        string serializedIntegrationEvent)
    {
        // Arrange
        var mockIIntegrationEventFactory = new Mock<IIntegrationEventFactory>();

        var mockIMediator = new Mock<IMediator>();

        var integrationEventReceiverService = new IntegrationEventReceiverService
        (
            mockIIntegrationEventFactory.Object,
            mockIMediator.Object
        );

        var expectedErrorMessage = $"{nameof(serializedIntegrationEvent)} is empty.";

        var integrationEventString = "integrationEventString";

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(() =>
            integrationEventReceiverService.ReceiveAsync(
                integrationEventString,
                serializedIntegrationEvent,
                CancellationToken.None
            )
        );

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentException>(actual);

        Assert.Equal(expectedErrorMessage, actual.Message);
    }
}