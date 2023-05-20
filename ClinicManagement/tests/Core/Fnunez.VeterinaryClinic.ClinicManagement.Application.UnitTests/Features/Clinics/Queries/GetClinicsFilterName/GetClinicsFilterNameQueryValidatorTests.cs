using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterName;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clinics.Queries.GetClinicsFilterName;

public class GetClinicsFilterNameQueryValidatorTests
{
    private readonly GetClinicsFilterNameQueryValidator _validator;

    public GetClinicsFilterNameQueryValidatorTests()
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

        var request = new GetClinicsFilterNameRequest
        {
            NameFilterValue = nameFilterValue
        };

        var query = new GetClinicsFilterNameQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsFilterNameRequest.NameFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_NameFilterValueIsEmpty_Fails(string nameFilterValue)
    {
        // Arrange
        var request = new GetClinicsFilterNameRequest
        {
            NameFilterValue = nameFilterValue
        };

        var query = new GetClinicsFilterNameQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsFilterNameRequest.NameFilterValue);
    }
}