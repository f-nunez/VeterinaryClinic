using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.UnitTests.AppointmentAggregate.ValueObjects;

public class DateTimeOffsetRangeTests
{
    [Fact]
    public void Constructor_StartOnIsGreaterThanOrEqualToEndOn_ThrowsArgumentException()
    {
        // Arrange
        var startOn = DateTimeOffset.UtcNow;

        var endOn = startOn;

        var errorMessage = $"StartOn: ({startOn}) should be less than EndOn: ({endOn})";

        // Act
        Action actual = () => new DateTimeOffsetRange(startOn, endOn);

        // Assert
        var error = Assert.Throws<ArgumentException>(actual);

        Assert.Equal(errorMessage, error.Message);
    }
}