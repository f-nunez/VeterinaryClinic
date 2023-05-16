using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Common.Exceptions;

public class SpecificationThenByExceptionTests
{
    [Fact]
    public void Constructor_PropertyName_CreatesAnExceptionWithSpecifiedErrorMessage()
    {
        // Arrange
        var propertyName = "a";

        var errorMessage = $"Property not found in the specification to be ordered then by {propertyName}.";

        // Act
        var actual = new SpecificationThenByException(propertyName);

        // Assert
        Assert.Equal(errorMessage, actual.Message);
    }
}