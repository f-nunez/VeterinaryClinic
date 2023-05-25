using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterId;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Clinics.Queries.GetClinicsFilterId;

public class GetClinicsFilterIdQueryValidatorTests
{
    private readonly GetClinicsFilterIdQueryValidator _validator;

    public GetClinicsFilterIdQueryValidatorTests()
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

        var request = new GetClinicsFilterIdRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetClinicsFilterIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsFilterIdRequest.IdFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_IdFilterValueIsEmpty_Fails(string idFilterValue)
    {
        // Arrange
        var request = new GetClinicsFilterIdRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetClinicsFilterIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetClinicsFilterIdRequest.IdFilterValue);
    }
}