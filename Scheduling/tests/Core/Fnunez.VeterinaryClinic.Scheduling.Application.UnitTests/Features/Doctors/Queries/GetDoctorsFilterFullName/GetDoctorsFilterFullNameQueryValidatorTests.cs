using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterFullName;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Doctors.Queries.GetDoctorsFilterFullName;

public class GetDoctorsFilterFullNameQueryValidatorTests
{
    private readonly GetDoctorsFilterFullNameQueryValidator _validator;

    public GetDoctorsFilterFullNameQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_FullNameFilterValueHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var fullNameFilterValue = string.Empty;

        for (int i = 0; i < 201; i++)
            fullNameFilterValue += "a";

        var request = new GetDoctorsFilterFullNameRequest
        {
            FullNameFilterValue = fullNameFilterValue
        };

        var query = new GetDoctorsFilterFullNameQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetDoctorsFilterFullNameRequest.FullNameFilterValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_FullFullNameFilterValueIsEmpty_Fails(
        string fullFullNameFilterValue)
    {
        // Arrange
        var request = new GetDoctorsFilterFullNameRequest
        {
            FullNameFilterValue = fullFullNameFilterValue
        };

        var query = new GetDoctorsFilterFullNameQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetDoctorsFilterFullNameRequest.FullNameFilterValue);
    }
}