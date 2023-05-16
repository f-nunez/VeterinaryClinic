using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRooms;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRooms;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Rooms.Queries.GetRooms;

public class GetRoomsQueryValidatorTests
{
    private readonly GetRoomsQueryValidator _validator;

    public GetRoomsQueryValidatorTests()
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

        var request = new GetRoomsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetRoomsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetRoomsRequest.DataGridRequest.Search);
    }

    [Fact]
    public void Validation_DataGridRequestSkipIsLessThanZero_Fails()
    {
        // Arrange
        int skip = -1;

        var dataGridRequest = new DataGridRequest { Skip = skip };

        var request = new GetRoomsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetRoomsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetRoomsRequest.DataGridRequest.Skip);
    }

    [Fact]
    public void Validation_DataGridRequestTakeIsGreaterThanOnehundred_Fails()
    {
        // Arrange
        int take = 101;

        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetRoomsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetRoomsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetRoomsRequest.DataGridRequest.Take);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_DataGridRequestTakeIsLessThanOrEqualToZero_Fails(
        int take)
    {
        // Arrange
        var dataGridRequest = new DataGridRequest { Take = take };

        var request = new GetRoomsRequest
        {
            DataGridRequest = dataGridRequest
        };

        var query = new GetRoomsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetRoomsRequest.DataGridRequest.Take);
    }

    [Fact]
    public void Validation_IdFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var idFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            idFilterValue += "a";

        var request = new GetRoomsRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetRoomsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetRoomsRequest.IdFilterValue);
    }

    [Fact]
    public void Validation_NameFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var nameFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            nameFilterValue += "a";

        var request = new GetRoomsRequest
        {
            NameFilterValue = nameFilterValue
        };

        var query = new GetRoomsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetRoomsRequest.NameFilterValue);
    }
}