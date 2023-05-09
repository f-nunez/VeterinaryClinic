using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterName;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterName;

public class GetAppointmentTypesFilterNameQueryValidatorTests
{
    private readonly GetAppointmentTypesFilterNameQueryValidator _validator;

    public GetAppointmentTypesFilterNameQueryValidatorTests()
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

        var request = new GetAppointmentTypesFilterNameRequest
        {
            NameFilterValue = nameFilterValue
        };

        var query = new GetAppointmentTypesFilterNameQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesFilterNameRequest.NameFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_NameFilterValueIsEmpty_Fails(string nameFilterValue)
    {
        // Arrange
        var request = new GetAppointmentTypesFilterNameRequest
        {
            NameFilterValue = nameFilterValue
        };

        var query = new GetAppointmentTypesFilterNameQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesFilterNameRequest.NameFilterValue);
    }
}