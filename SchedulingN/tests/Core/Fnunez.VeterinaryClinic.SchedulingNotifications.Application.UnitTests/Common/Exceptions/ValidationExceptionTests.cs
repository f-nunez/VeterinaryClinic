using FluentValidation.Results;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Exceptions;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void Constructor_Failures_CreatesAnErrorsDictionaryContainingValidationFailures()
    {
        // Arrange
        var propertyA = "propertyA";
        var propertyB = "propertyB";
        var exceptionMessageA = "Exception message a";
        var exceptionMessageB = "Exception message b";
        var exceptionMessageC = "Exception message c";

        var validationVailures = new List<ValidationFailure>
        {
            new ValidationFailure(propertyA, exceptionMessageA),
            new ValidationFailure(propertyB, exceptionMessageB),
            new ValidationFailure(propertyB, exceptionMessageC)
        };

        var validationException = new ValidationException(validationVailures);

        // Act
        var actual = validationException.Errors;

        // Assert
        Assert.Equal(new string[] { propertyA, propertyB }, actual.Keys);

        Assert.Equal(
            new string[] { exceptionMessageA },
            actual[propertyA]
        );

        Assert.Equal(
            new string[] { exceptionMessageB, exceptionMessageC },
            actual[propertyB]
        );

        Assert.IsType<Dictionary<string, string[]>>(actual);
    }

    [Fact]
    public void Constructor_WithoutParameters_CreatesAnEmptyErrorsDictionary()
    {
        // Arrange
        var validationException = new ValidationException();

        // Act
        var actual = validationException.Errors;

        // Assert
        Assert.Empty(actual);
        Assert.IsType<Dictionary<string, string[]>>(actual);
    }
}