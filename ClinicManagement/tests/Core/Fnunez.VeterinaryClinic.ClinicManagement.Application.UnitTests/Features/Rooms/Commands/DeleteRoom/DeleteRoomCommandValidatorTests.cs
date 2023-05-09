using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.DeleteRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommandValidatorTests
{
    private readonly DeleteRoomCommandValidator _validator;

    public DeleteRoomCommandValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_IdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int id = 1;

        var request = new DeleteRoomRequest { Id = id };

        var command = new DeleteRoomCommand(request);

        //Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.DeleteRoomRequest.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_IdIsLessOrEqualsThanZero_Fails(int id)
    {
        // Arrange
        var request = new DeleteRoomRequest { Id = id };

        var command = new DeleteRoomCommand(request);

        //Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.DeleteRoomRequest.Id);
    }
}