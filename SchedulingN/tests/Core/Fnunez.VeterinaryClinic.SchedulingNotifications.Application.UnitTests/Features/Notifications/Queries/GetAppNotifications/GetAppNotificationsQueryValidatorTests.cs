using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries.GetAppNotifications;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.UnitTests.Features.Notifications.Queries.GetAppNotifications;

public class GetAppNotificationsQueryValidatorTests
{
    private readonly GetAppNotificationsQueryValidator _validator;

    public GetAppNotificationsQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_SkipIsLessThanZero_Fails()
    {
        // Arrange
        int skip = -1;

        var request = new GetAppNotificationsRequest { Skip = skip };

        var query = new GetAppNotificationsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetNotificationsRequest.Skip);
    }

    [Fact]
    public void Validation_TakeIsGreaterThanOnehundred_Fails()
    {
        // Arrange
        int take = 101;

        var request = new GetAppNotificationsRequest { Take = take };

        var query = new GetAppNotificationsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetNotificationsRequest.Take);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_TakeIsLessThanOrEqualToZero_Fails(
        int take)
    {
        // Arrange
        var request = new GetAppNotificationsRequest { Take = take };

        var query = new GetAppNotificationsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetNotificationsRequest.Take);
    }
}