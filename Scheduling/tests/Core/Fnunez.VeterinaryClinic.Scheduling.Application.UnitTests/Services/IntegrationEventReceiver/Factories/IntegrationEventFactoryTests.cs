using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.Factories;
using Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Services.IntegrationEventReceiver.Factories;

public class IntegrationEventFactoryTests
{
    private readonly IntegrationEventFactory _factory;

    public IntegrationEventFactoryTests()
    {
        _factory = new();
    }

    [Fact]
    public void GetDeserializedIntegrationEvent_AllIntegrationEventsAreFound_IsValid()
    {
        // Arrange
        var serializedIntegrationEvent = "serializedIntegrationEvent";

        var allIntegrationEventss = Enum.GetValues(typeof(IntegrationEvent))
            .Cast<IntegrationEvent>();

        var allIntegrationEventssAreFound = true;

        // Act
        foreach (var integrationEvent in allIntegrationEventss)
        {
            try
            {
                _factory.GetDeserializedIntegrationEvent(
                    integrationEvent, serializedIntegrationEvent);
            }
            catch (Exception ex)
            {
                if (ex.Message == $"{nameof(integrationEvent)} not found with value: {integrationEvent}")
                {
                    allIntegrationEventssAreFound = false;
                    break;
                }
            }
        }

        // Assert
        Assert.True(allIntegrationEventssAreFound);
    }

    [Fact]
    public void GetReceiveIntegrationEvent_AllIntegrationEventsAreFound_IsValid()
    {
        // Arrange
        var mockBaseIntegrationEvent = new Mock<BaseIntegrationEvent>();

        var allIntegrationEventss = Enum.GetValues(typeof(IntegrationEvent))
            .Cast<IntegrationEvent>();

        var allIntegrationEventssAreFound = true;

        // Act
        foreach (var integrationEvent in allIntegrationEventss)
        {
            try
            {
                _factory.GetReceiveIntegrationEvent(integrationEvent, mockBaseIntegrationEvent.Object);
            }
            catch (Exception ex)
            {
                if (ex.Message == $"{nameof(integrationEvent)} not found with value: {integrationEvent}")
                {
                    allIntegrationEventssAreFound = false;
                    break;
                }
            }
        }

        // Assert
        Assert.True(allIntegrationEventssAreFound);
    }
}