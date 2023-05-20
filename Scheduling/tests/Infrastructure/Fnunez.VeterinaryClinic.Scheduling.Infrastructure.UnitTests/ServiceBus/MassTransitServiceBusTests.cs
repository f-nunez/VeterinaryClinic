using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Interfaces;
using Fnunez.VeterinaryClinic.Scheduling.Infrastructure.ServiceBus;
using MassTransit;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.UnitTests.ServiceBus;

public class MassTransitServiceBusTests
{
    [Fact]
    public async void PublishAsync_CallsPublishMethodOnceFromPublishEndpoint()
    {
        // Arrange
        var mockIPublishEndpoint = new Mock<IPublishEndpoint>();

        mockIPublishEndpoint.Setup(x =>
            x.Publish(
                It.IsAny<object>(),
                CancellationToken.None
            )
        );

        var mockMassTransitServiceBus = new Mock<MassTransitServiceBus>(
            mockIPublishEndpoint.Object);

        // Act
        await mockMassTransitServiceBus.Object.PublishAsync(
            new object { }, CancellationToken.None);

        // Assert
        mockIPublishEndpoint.Verify(x =>
            x.Publish(
                It.IsAny<object>(),
                CancellationToken.None
            ),
            Times.Once()
        );
    }

    [Fact]
    public async void PublishAsync_MessageIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        object? message = null;

        var mockIPublishEndpoint = new Mock<IPublishEndpoint>();

        IServiceBus serviceBus;

        serviceBus = new MassTransitServiceBus(mockIPublishEndpoint.Object);

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentNullException>(() =>
            serviceBus.PublishAsync(message, CancellationToken.None));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ArgumentNullException>(actual);
    }
}