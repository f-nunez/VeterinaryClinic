using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Exceptions;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.UnitTests.Common.Exceptions;

public class NotFoundExceptionTests
{
    [Fact]
    public void Constructor_Message_CreatesAnExceptionWithSpecifiedErrorMessage()
    {
        // Arrange
        var errorMessage = "Message a";

        // Act
        var actual = new NotFoundException(errorMessage);

        // Assert
        Assert.Equal(errorMessage, actual.Message);
    }

    [Fact]
    public void Constructor_MessageAndInnerException_CreatesAnExceptionWithSpecifiedErrorMessageAndTheInnerException()
    {
        // Arrange
        var errorMessage = "Message a";

        var exception = new Exception(errorMessage);

        // Act
        var actual = new NotFoundException(errorMessage, exception);

        // Assert
        Assert.Equal(errorMessage, actual.Message);
        Assert.Equal(exception, actual.InnerException);
    }
}