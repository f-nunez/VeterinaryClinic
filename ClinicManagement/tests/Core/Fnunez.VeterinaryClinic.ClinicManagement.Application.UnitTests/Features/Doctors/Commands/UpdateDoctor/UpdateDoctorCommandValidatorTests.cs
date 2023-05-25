using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Commands.UpdateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;

public class UpdateDoctorCommandValidatorTests
{
    private readonly UpdateDoctorCommandValidator _validator;

    public UpdateDoctorCommandValidatorTests()
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

        var request = new UpdateDoctorRequest { FullName = fullName };

        var command = new UpdateDoctorCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateDoctorRequest.FullName);
    }

    [Fact]
    public void Validation_FullNameHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var fullName = string.Empty;

        for (int i = 0; i < 201; i++)
            fullName += "a";

        var request = new UpdateDoctorRequest { FullName = fullName };

        var command = new UpdateDoctorCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateDoctorRequest.FullName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_FullNameIsEmpty_Fails(string fullName)
    {
        // Arrange
        var request = new UpdateDoctorRequest { FullName = fullName };

        var command = new UpdateDoctorCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateDoctorRequest.FullName);
    }

    [Fact]
    public void Validation_IdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int id = 1;

        var request = new UpdateDoctorRequest { Id = id };

        var command = new UpdateDoctorCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateDoctorRequest.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_IdIsLessThanOrEqualToZero_Fails(int id)
    {
        // Arrange
        var request = new UpdateDoctorRequest { Id = id };

        var command = new UpdateDoctorCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateDoctorRequest.Id);
    }
}