using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterId;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Doctors.Queries.GetDoctorsFilterId;

public class GetDoctorsFilterIdQueryValidatorTests
{
    private readonly GetDoctorsFilterIdQueryValidator _validator;

    public GetDoctorsFilterIdQueryValidatorTests()
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

        var request = new GetDoctorsFilterIdRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetDoctorsFilterIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetDoctorsFilterIdRequest.IdFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_IdFilterValueIsEmpty_Fails(string idFilterValue)
    {
        // Arrange
        var request = new GetDoctorsFilterIdRequest
        {
            IdFilterValue = idFilterValue
        };

        var query = new GetDoctorsFilterIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetDoctorsFilterIdRequest.IdFilterValue);
    }
}