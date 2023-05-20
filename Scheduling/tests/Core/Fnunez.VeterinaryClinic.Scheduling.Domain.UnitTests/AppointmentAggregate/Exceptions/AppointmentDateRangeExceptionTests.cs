using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate.Exceptions;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.UnitTests.AppointmentAggregate.Exceptions;

public class AppointmentDateRangeExceptionTests
{
    [Fact]
    public void Constructor_CreatesAnExceptionWithSpecifiedErrorMessage()
    {
        // Arrange
        var startOn = DateTimeOffset.UtcNow;

        var endOn = startOn;

        var errorMessage = $"StartOn: ({startOn.ToString()}) must be less than EndOn: {endOn.ToString()}";

        // Act
        var actual = new AppointmentDateRangeException(startOn, endOn);

        // Assert
        Assert.Equal(errorMessage, actual.Message);
    }
}