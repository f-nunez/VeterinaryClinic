using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterEmailAddress;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterEmailAddress;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clinics.Queries.GetclinicsFilterEmailAddress;

public class GetClinicsFilterEmailAddressQueryValidatorTests
{
    private readonly GetClinicsFilterEmailAddressQueryValidator _validator;

    public GetClinicsFilterEmailAddressQueryValidatorTests()
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

        var request = new GetClinicsFilterEmailAddressRequest
        {
            EmailAddressFilterValue = emailAddressFilterValue
        };

        var query = new GetClinicsFilterEmailAddressQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsFilterEmailAddressRequest.EmailAddressFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_EmailAddressFilterValueIsEmpty_Fails(
        string emailAddressFilterValue)
    {
        // Arrange
        var request = new GetClinicsFilterEmailAddressRequest
        {
            EmailAddressFilterValue = emailAddressFilterValue
        };

        var query = new GetClinicsFilterEmailAddressQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsFilterEmailAddressRequest.EmailAddressFilterValue);
    }
}