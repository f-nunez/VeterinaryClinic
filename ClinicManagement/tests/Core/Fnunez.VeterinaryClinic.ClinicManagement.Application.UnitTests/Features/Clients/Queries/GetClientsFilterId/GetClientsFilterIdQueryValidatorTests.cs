using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterId;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clients.Queries.GetClientsFilterId;

public class GetClientsFilterIdQueryValidatorTests
{
    private readonly GetClientsFilterIdQueryValidator _validator;

    public GetClientsFilterIdQueryValidatorTests()
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

        var request = new GetClientsFilterIdRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetClientsFilterIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsFilterIdRequest.IdFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_IdFilterValueIsEmpty_Fails(string idFilterValue)
    {
        // Arrange
        var request = new GetClientsFilterIdRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetClientsFilterIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsFilterIdRequest.IdFilterValue);
    }
}