using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterEmailAddress;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clients.Queries.GetClientsFilterEmailAddress;

public class GetClientsFilterEmailAddressQueryValidatorTests
{
    private readonly GetClientsFilterEmailAddressQueryValidator _validator;

    public GetClientsFilterEmailAddressQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_EmailAddressFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var emailAddressFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            emailAddressFilterValue += "a";

        var request = new GetClientsFilterEmailAddressRequest
        {
            EmailAddressFilterValue = emailAddressFilterValue
        };

        var query = new GetClientsFilterEmailAddressQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsFilterEmailAddressRequest.EmailAddressFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_EmailAddressFilterValueIsEmpty_Fails(
        string emailAddressFilterValue)
    {
        // Arrange
        var request = new GetClientsFilterEmailAddressRequest
        {
            EmailAddressFilterValue = emailAddressFilterValue
        };

        var query = new GetClientsFilterEmailAddressQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClientsFilterEmailAddressRequest.EmailAddressFilterValue);
    }
}