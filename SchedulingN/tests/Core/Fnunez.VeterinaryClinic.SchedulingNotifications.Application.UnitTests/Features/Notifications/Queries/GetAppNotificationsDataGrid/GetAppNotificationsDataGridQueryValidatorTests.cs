using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Common.Requests;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Features.Notifications.Queries.GetAppNotificationsDataGrid;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.UnitTests.Features.Notifications.Queries.GetAppNotificationsDataGrid;

public class GetAppNotificationsDataGridQueryValidatorTests
{
    private readonly GetAppNotificationsDataGridQueryValidator _validator;

    public GetAppNotificationsDataGridQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_DataGridRequestSearchHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var search = string.Empty;

        for (int i = 0; i < 201; i++)
            search += "a";

        var dataGridRequest = new DataGridRequest { Search = search };

        var request = new GetAppNotificationsDataGridRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetAppNotificationsDataGridQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppNotificationsDataGridRequest.DataGridRequest.Search);
    }

    [Fact]
    public void Validation_DataGridRequestSkipIsLessThanZero_Fails()
    {
        // Arrange
        int skip = -1;

        var dataGridRequest = new DataGridRequest { Skip = skip };

        var request = new GetAppNotificationsDataGridRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetAppNotificationsDataGridQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppNotificationsDataGridRequest.DataGridRequest.Skip);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsGreaterThanOnehundred_Fails()
    {
        // Arrange
        int take = 101;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetAppNotificationsDataGridRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetAppNotificationsDataGridQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppNotificationsDataGridRequest.DataGridRequest.Take);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_DataGridRequestTakeIsLessThanOrEqualToZero_Fails(
        int take)
    {
        // Arrange
        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetAppNotificationsDataGridRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetAppNotificationsDataGridQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppNotificationsDataGridRequest.DataGridRequest.Take);
    }
}