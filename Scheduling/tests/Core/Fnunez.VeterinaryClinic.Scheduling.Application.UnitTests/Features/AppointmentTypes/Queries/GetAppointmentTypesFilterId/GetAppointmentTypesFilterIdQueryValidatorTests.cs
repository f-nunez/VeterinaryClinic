using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterId;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterId;

public class GetAppointmentTypesFilterIdQueryValidatorTests
{
    private readonly GetAppointmentTypesFilterIdQueryValidator _validator;

    public GetAppointmentTypesFilterIdQueryValidatorTests()
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

        var request = new GetAppointmentTypesFilterIdRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetAppointmentTypesFilterIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesFilterIdRequest.IdFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_IdFilterValueIsEmpty_Fails(
        string IdFilterValue)
    {
        // Arrange
        var request = new GetAppointmentTypesFilterIdRequest
        {
            IdFilterValue = IdFilterValue
        };

        var query = new GetAppointmentTypesFilterIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypesFilterIdRequest.IdFilterValue);
    }
}