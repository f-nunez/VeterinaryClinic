using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterDuration;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterDuration;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterDuration;

public class GetAppointmentTypesFilterDurationQueryValidatorTests
{
    private readonly GetAppointmentTypesFilterDurationQueryValidator _validator;

    public GetAppointmentTypesFilterDurationQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_DurationFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var durationFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            durationFilterValue += "a";

        var request = new GetAppointmentTypesFilterDurationRequest
        {
            DurationFilterValue = durationFilterValue
        };

        var query = new GetAppointmentTypesFilterDurationQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesFilterDurationRequest.DurationFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_DurationFilterValueIsEmpty_Fails(
        string durationFilterValue)
    {
        // Arrange
        var request = new GetAppointmentTypesFilterDurationRequest
        {
            DurationFilterValue = durationFilterValue
        };

        var query = new GetAppointmentTypesFilterDurationQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesFilterDurationRequest.DurationFilterValue);
    }
}