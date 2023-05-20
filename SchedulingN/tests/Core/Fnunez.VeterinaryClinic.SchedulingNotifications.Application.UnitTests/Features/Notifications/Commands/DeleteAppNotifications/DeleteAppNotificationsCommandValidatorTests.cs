using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Commands.DeleteAppNotifications;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.UnitTests.Features.Notifications.Commands.DeleteAppNotifications;

public class DeleteAppNotificationsCommandValidatorTests
{
    private readonly DeleteAppNotificationsCommandValidator _validator;

    public DeleteAppNotificationsCommandValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_AppNotificationIdsIsEmpty_Fails()
    {
        // Arrange
        List<Guid> appNotificationIds = new();

        var request = new DeleteAppNotificationsRequest
        {
            AppNotificationIds = appNotificationIds
        };

        var command = new DeleteAppNotificationsCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.DeleteAppNotificationsRequest.AppNotificationIds);
    }
}