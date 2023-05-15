using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.UpdateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Rooms.Commands.UpdateRoom;

public class UpdateRoomCommandValidatorTests
{
    private readonly UpdateRoomCommandValidator _validator;

    public UpdateRoomCommandValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_IdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int id = 1;

        var request = new UpdateRoomRequest { Id = id };

        var command = new UpdateRoomCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateRoomRequest.Id);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(200)]
    public void Validation_NameHasCharactersBetweenOneAndTwoHundred_IsValid(
        int nameLength)
    {
        // Arrange
        var name = string.Empty;

        for (int i = 0; i < nameLength; i++)
            name += "a";

        var request = new UpdateRoomRequest { Name = name };

        var command = new UpdateRoomCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateRoomRequest.Name);
    }

    [Fact]
    public void Validation_NameHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var name = string.Empty;

        for (int i = 0; i < 201; i++)
            name += "a";

        var request = new UpdateRoomRequest { Name = name };

        var command = new UpdateRoomCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateRoomRequest.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_NameIsEmpty_Fails(string name)
    {
        // Arrange
        var request = new UpdateRoomRequest { Name = name };

        var command = new UpdateRoomCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateRoomRequest.Name);
    }
}