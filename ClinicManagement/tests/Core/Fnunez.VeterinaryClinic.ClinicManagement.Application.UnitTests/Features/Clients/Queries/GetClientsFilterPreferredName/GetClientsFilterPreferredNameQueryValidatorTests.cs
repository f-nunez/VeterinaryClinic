using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredName;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clients.Queries.GetClientsFilterPreferredName;

public class GetClientsFilterPreferredNameQueryValidatorTests
{
    private readonly GetClientsFilterPreferredNameQueryValidator _validator;

    public GetClientsFilterPreferredNameQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_PreferredNameFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var preferredNameFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            preferredNameFilterValue += "a";

        var request = new GetClientsFilterPreferredNameRequest
        {
            PreferredNameFilterValue = preferredNameFilterValue
        };

        var query = new GetClientsFilterPreferredNameQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsFilterPreferredNameRequest.PreferredNameFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_PreferredNameFilterValueIsEmpty_Fails(
        string preferredNameFilterValue)
    {
        // Arrange
        var request = new GetClientsFilterPreferredNameRequest
        {
            PreferredNameFilterValue = preferredNameFilterValue
        };

        var query = new GetClientsFilterPreferredNameQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsFilterPreferredNameRequest.PreferredNameFilterValue);
    }
}