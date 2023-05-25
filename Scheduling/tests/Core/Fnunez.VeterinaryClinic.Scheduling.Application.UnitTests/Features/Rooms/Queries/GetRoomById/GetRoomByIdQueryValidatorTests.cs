using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomById;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryValidatorTests
{
    private readonly GetRoomByIdQueryValidator _validator;

    public GetRoomByIdQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_IdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int id = 1;

        var request = new GetRoomByIdRequest { Id = id };

        var query = new GetRoomByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetRoomByIdRequest.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_IdIsLessThanOrEqualToZero_Fails(int id)
    {
        // Arrange
        var request = new GetRoomByIdRequest { Id = id };

        var query = new GetRoomByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetRoomByIdRequest.Id);
    }
}