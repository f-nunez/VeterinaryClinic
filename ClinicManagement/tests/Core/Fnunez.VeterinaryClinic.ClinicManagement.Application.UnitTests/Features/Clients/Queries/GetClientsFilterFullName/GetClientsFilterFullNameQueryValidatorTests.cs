using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterFullName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterFullName;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clients.Queries;

public class GetClientsFilterFullNameQueryValidatorTests
{
    private readonly GetClientsFilterFullNameQueryValidator _validator;

    public GetClientsFilterFullNameQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_FullNameFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var fullNameFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            fullNameFilterValue += "a";

        var request = new GetClientsFilterFullNameRequest
        {
            FullNameFilterValue = fullNameFilterValue
        };

        var query = new GetClientsFilterFullNameQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsFilterFullNameRequest.FullNameFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_FullNameFilterValueIsEmpty_Fails(
        string fullNameFilterValue)
    {
        // Arrange
        var request = new GetClientsFilterFullNameRequest
        {
            FullNameFilterValue = fullNameFilterValue
        };

        var query = new GetClientsFilterFullNameQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsFilterFullNameRequest.FullNameFilterValue);
    }
}