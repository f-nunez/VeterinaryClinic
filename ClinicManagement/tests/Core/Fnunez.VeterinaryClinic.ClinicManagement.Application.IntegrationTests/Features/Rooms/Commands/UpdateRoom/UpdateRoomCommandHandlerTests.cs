using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.UpdateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Rooms.Commands.UpdateRoom;

[Collection(nameof(TestContextFixture))]
public class UpdateRoomCommandHandlerTests : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public UpdateRoomCommandHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new UpdateRoomRequest();

        var command = new UpdateRoomCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsUpdateRoomResponse()
    {
        // Arrange
        var name = "a";

        var room = new Room(name);

        await _fixture.AddAsync<Room>(room);

        name = "aa";

        var request = new UpdateRoomRequest
        {
            Id = room.Id,
            Name = name
        };

        var command = new UpdateRoomCommand(request);

        // Act
        var actual = await _fixture.SendAsync(command);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<UpdateRoomResponse>(actual);

        Assert.Equal(name, actual.Room.Name);
    }
}