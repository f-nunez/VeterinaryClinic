using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.AppointmentTypes.Commands.CreateAppointmentType;

public class CreateAppointmentTypeCommandValidatorTests
{
    private readonly CreateAppointmentTypeCommandValidator _validator;

    public CreateAppointmentTypeCommandValidatorTests()
    {
        _validator = new();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    public void Validation_CodeHasCharactersBetweenOneAndOnehundred_IsValid(
        int codeLength)
    {
        // Arrange
        var code = string.Empty;

        for (int i = 0; i < codeLength; i++)
            code += "a";

        var request = new CreateAppointmentTypeRequest { Code = code };

        var command = new CreateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentTypeRequest.Code);
    }

    [Fact]
    public void Validation_CodeHasMoreThanOneHundredCharacters_Fails()
    {
        // Arrange
        var code = string.Empty;

        for (int i = 0; i < 101; i++)
            code += "a";

        var request = new CreateAppointmentTypeRequest { Code = code };

        var command = new CreateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentTypeRequest.Code);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_CodeIsEmpty_Fails(
        string code)
    {
        // Arrange
        var request = new CreateAppointmentTypeRequest { Code = code };

        var command = new CreateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentTypeRequest.Code);
    }

    [Fact]
    public void Validation_DurationIsGreaterThanZero_IsValid()
    {
        // Arrange
        int duration = 1;

        var request = new CreateAppointmentTypeRequest { Duration = duration };

        var command = new CreateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentTypeRequest.Duration);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_DurationIsLessOrEqualsThanZero_Fails(int duration)
    {
        // Arrange
        var request = new CreateAppointmentTypeRequest { Duration = duration };

        var command = new CreateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentTypeRequest.Duration);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(200)]
    public void Validation_NameHasBetweenOneAndTwoHundredCharacters_IsValid(
        int nameLength)
    {
        // Arrange
        var name = string.Empty;

        for (int i = 0; i < nameLength; i++)
            name += "a";

        var request = new CreateAppointmentTypeRequest { Name = name };

        var command = new CreateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentTypeRequest.Name);
    }

    [Fact]
    public void Validation_NameHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var name = string.Empty;

        for (int i = 0; i < 201; i++)
            name += "a";

        var request = new CreateAppointmentTypeRequest { Name = name };

        var command = new CreateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentTypeRequest.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_NameIsEmpty_Fails(
        string name)
    {
        // Arrange
        var request = new CreateAppointmentTypeRequest { Code = name };

        var command = new CreateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentTypeRequest.Name);
    }
}