using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterSalutation;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterSalutation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clients.Queries.GetClientsFilterSalutation;

public class GetClientsFilterSalutationQueryValidatorTests
{
    private readonly GetClientsFilterSalutationQueryValidator _validator;

    public GetClientsFilterSalutationQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_SalutationFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var salutationFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            salutationFilterValue += "a";

        var request = new GetClientsFilterSalutationRequest
        {
            SalutationFilterValue = salutationFilterValue
        };

        var query = new GetClientsFilterSalutationQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsFilterSalutationRequest.SalutationFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_SalutationFilterValueIsEmpty_Fails(
        string salutationFilterValue)
    {
        // Arrange
        var request = new GetClientsFilterSalutationRequest
        {
            SalutationFilterValue = salutationFilterValue
        };

        var query = new GetClientsFilterSalutationQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsFilterSalutationRequest.SalutationFilterValue);
    }
}