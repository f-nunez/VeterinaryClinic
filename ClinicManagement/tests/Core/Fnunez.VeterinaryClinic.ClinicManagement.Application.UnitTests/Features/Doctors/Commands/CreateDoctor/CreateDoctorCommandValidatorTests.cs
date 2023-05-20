using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Doctors.Commands.CreateDoctor;

public class CreateDoctorCommandValidatorTests
{
    private readonly CreateDoctorCommandValidator _validator;

    public CreateDoctorCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(200)]
    public void Validation_FullNameHasCharactersBetweenOneAndTwoHundred_IsValid(
        int fullNameLength)
    {
        // Arrange
        var fullName = string.Empty;

        for (int i = 0; i < fullNameLength; i++)
            fullName += "a";

        var request = new CreateDoctorRequest { FullName = fullName };

        var command = new CreateDoctorCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateDoctorRequest.FullName);
    }

    [Fact]
    public void Validation_FullNameHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var fullName = string.Empty;

        for (int i = 0; i < 201; i++)
            fullName += "a";

        var request = new CreateDoctorRequest { FullName = fullName };

        var command = new CreateDoctorCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateDoctorRequest.FullName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_FullNameIsEmpty_Fails(string fullName)
    {
        // Arrange
        var request = new CreateDoctorRequest { FullName = fullName };

        var command = new CreateDoctorCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateDoctorRequest.FullName);
    }
}