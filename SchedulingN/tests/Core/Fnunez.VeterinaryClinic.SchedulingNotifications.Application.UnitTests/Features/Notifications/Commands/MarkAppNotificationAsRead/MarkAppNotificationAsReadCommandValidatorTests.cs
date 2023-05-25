using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.MarkAppNotificationAsRead;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.UnitTests.Features.Notifications.Commands.MarkAppNotificationAsRead;

public class MarkAppNotificationAsReadCommandValidatorTests
{
    private readonly MarkAppNotificationAsReadCommandValidator _validator;

    public MarkAppNotificationAsReadCommandValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_AppNotificationIdIsEmpty_Fails()
    {
        // Arrange
        var appNotificationId = Guid.Empty;

        var request = new MarkAppNotificationAsReadRequest
        {
            AppNotificationId = appNotificationId
        };

        var command = new MarkAppNotificationAsReadCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.MarkNotificationAsReadRequest.AppNotificationId);
    }
}