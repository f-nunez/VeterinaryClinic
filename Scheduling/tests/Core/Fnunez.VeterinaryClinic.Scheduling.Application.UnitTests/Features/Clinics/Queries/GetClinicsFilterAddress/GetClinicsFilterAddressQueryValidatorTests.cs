using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterAddress;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterAddress;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Clinics.Queries.GetClinicsFilterAddress;

public class GetClinicsFilterAddressQueryValidatorTests
{
    private readonly GetClinicsFilterAddressQueryValidator _validator;

    public GetClinicsFilterAddressQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_AddressFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var addressFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            addressFilterValue += "a";

        var request = new GetClinicsFilterAddressRequest
        {
            AddressFilterValue = addressFilterValue
        };

        var query = new GetClinicsFilterAddressQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsFilterAddressRequest.AddressFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_AddressFilterValueIsEmpty_Fails(
        string addressFilterValue)
    {
        // Arrange
        var request = new GetClinicsFilterAddressRequest
        {
            AddressFilterValue = addressFilterValue
        };

        var query = new GetClinicsFilterAddressQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsFilterAddressRequest.AddressFilterValue);
    }
}