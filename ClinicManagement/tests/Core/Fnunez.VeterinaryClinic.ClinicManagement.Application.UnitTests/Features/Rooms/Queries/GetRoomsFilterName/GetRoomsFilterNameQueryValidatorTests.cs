using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRoomsFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomsFilterName;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Rooms.Queries.GetRoomsFilterName;

public class GetRoomsFilterNameQueryValidatorTests
{
    private readonly GetRoomsFilterNameQueryValidator _validator;

    public GetRoomsFilterNameQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_NameFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var nameFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            nameFilterValue += "a";

        var request = new GetRoomsFilterNameRequest
        {
            NameFilterValue = nameFilterValue
        };

        var query = new GetRoomsFilterNameQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetRoomsFilterNameRequest.NameFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_NameFilterValueIsEmpty_Fails(string nameFilterValue)
    {
        // Arrange
        var request = new GetRoomsFilterNameRequest
        {
            NameFilterValue = nameFilterValue
        };

        var query = new GetRoomsFilterNameQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetRoomsFilterNameRequest.NameFilterValue);
    }
}