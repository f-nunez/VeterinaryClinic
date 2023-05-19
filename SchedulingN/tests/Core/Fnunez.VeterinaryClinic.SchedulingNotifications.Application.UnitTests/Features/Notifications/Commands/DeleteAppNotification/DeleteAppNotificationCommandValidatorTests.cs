using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotification;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.UnitTests.Features.Notifications.Commands.DeleteAppNotification;

public class DeleteAppNotificationCommandValidatorTests
{
    private readonly DeleteAppNotificationCommandValidator _validator;

    public DeleteAppNotificationCommandValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_AppNotificationIdIsEmpty_Fails()
    {
        // Arrange
        var appNotificationId = Guid.Empty;

        var request = new DeleteAppNotificationRequest
        {
            AppNotificationId = appNotificationId
        };

        var command = new DeleteAppNotificationCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.DeleteAppNotificationRequest.AppNotificationId);
    }
}