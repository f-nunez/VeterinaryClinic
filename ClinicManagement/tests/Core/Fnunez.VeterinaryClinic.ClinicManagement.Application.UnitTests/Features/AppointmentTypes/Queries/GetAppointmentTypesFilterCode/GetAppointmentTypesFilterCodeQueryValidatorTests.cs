using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterCode;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterCode;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterCode;

public class GetAppointmentTypesFilterCodeQueryValidatorTests
{
    private readonly GetAppointmentTypesFilterCodeQueryValidator _validator;

    public GetAppointmentTypesFilterCodeQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_CodeFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var codeFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            codeFilterValue += "a";

        var request = new GetAppointmentTypesFilterCodeRequest
        {
            CodeFilterValue = codeFilterValue
        };

        var query = new GetAppointmentTypesFilterCodeQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesFilterCodeRequest.CodeFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_CodeFilterValueIsEmpty_Fails(string codeFilterValue)
    {
        // Arrange
        var request = new GetAppointmentTypesFilterCodeRequest
        {
            CodeFilterValue = codeFilterValue
        };

        var query = new GetAppointmentTypesFilterCodeQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesFilterCodeRequest.CodeFilterValue);
    }
}