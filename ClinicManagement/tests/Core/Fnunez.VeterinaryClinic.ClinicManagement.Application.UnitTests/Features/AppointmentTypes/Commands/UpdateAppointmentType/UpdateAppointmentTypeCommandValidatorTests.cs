using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.UpdateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.UpdateAppointmentType;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.AppointmentTypes.Commands.UpdateAppointmentType;

public class UpdateAppointmentTypeCommandValidatorTests
{
    private readonly UpdateAppointmentTypeCommandValidator _validator;

    public UpdateAppointmentTypeCommandValidatorTests()
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

        var request = new UpdateAppointmentTypeRequest { Code = code };

        var command = new UpdateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentTypeRequest.Code);
    }

    [Fact]
    public void Validation_CodeHasMoreThanOneHundredCharacters_Fails()
    {
        // Arrange
        var code = string.Empty;

        for (int i = 0; i < 101; i++)
            code += "a";

        var request = new UpdateAppointmentTypeRequest { Code = code };

        var command = new UpdateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentTypeRequest.Code);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_CodeIsEmpty_Fails(
        string code)
    {
        // Arrange
        var request = new UpdateAppointmentTypeRequest { Code = code };

        var command = new UpdateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentTypeRequest.Code);
    }

    [Fact]
    public void Validation_DurationIsGreaterThanZero_IsValid()
    {
        // Arrange
        int duration = 1;

        var request = new UpdateAppointmentTypeRequest { Duration = duration };

        var command = new UpdateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentTypeRequest.Duration);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_DurationIsLessThanOrEqualToZero_Fails(int duration)
    {
        // Arrange
        var request = new UpdateAppointmentTypeRequest { Duration = duration };

        var command = new UpdateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentTypeRequest.Duration);
    }

    [Fact]
    public void Validation_IdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int id = 1;

        var request = new UpdateAppointmentTypeRequest { Id = id };

        var command = new UpdateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentTypeRequest.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_IdIsLessThanOrEqualToZero_Fails(int id)
    {
        // Arrange
        var request = new UpdateAppointmentTypeRequest { Id = id };

        var command = new UpdateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentTypeRequest.Id);
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

        var request = new UpdateAppointmentTypeRequest { Name = name };

        var command = new UpdateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentTypeRequest.Name);
    }

    [Fact]
    public void Validation_NameHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var name = string.Empty;

        for (int i = 0; i < 201; i++)
            name += "a";

        var request = new UpdateAppointmentTypeRequest { Name = name };

        var command = new UpdateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentTypeRequest.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_NameIsEmpty_Fails(
        string name)
    {
        // Arrange
        var request = new UpdateAppointmentTypeRequest { Code = name };

        var command = new UpdateAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentTypeRequest.Name);
    }
}