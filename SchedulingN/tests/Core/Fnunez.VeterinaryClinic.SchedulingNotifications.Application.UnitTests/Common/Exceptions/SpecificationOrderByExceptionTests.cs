using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Exceptions;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.UnitTests.Common.Exceptions;

public class SpecificationOrderByExceptionTests
{
    [Fact]
    public void Constructor_PropertyName_CreatesAnExceptionWithSpecifiedErrorMessage()
    {
        // Arrange
        var propertyName = "a";

        var errorMessage = $"Property not found in the specification to be ordered by {propertyName}.";

        // Act
        var actual = new SpecificationOrderByException(propertyName);

        // Assert
        Assert.Equal(errorMessage, actual.Message);
    }
}