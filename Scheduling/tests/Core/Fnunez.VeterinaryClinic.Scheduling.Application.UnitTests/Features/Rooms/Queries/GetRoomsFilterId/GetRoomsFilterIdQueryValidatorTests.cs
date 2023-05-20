using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterId;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Rooms.Queries.GetRoomsFilterId;

public class GetRoomsFilterIdQueryValidatorTests
{
    private readonly GetRoomsFilterIdQueryValidator _validator;

    public GetRoomsFilterIdQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_IdFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var idFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            idFilterValue += "a";

        var request = new GetRoomsFilterIdRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetRoomsFilterIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetRoomsFilterIdRequest.IdFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_IdFilterValueIsEmpty_Fails(string idFilterValue)
    {
        // Arrange
        var request = new GetRoomsFilterIdRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetRoomsFilterIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetRoomsFilterIdRequest.IdFilterValue);
    }
}