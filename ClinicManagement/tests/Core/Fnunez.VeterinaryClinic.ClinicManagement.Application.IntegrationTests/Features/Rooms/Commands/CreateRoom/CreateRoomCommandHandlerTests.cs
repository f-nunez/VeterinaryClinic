using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Commands.CreateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Rooms.Commands.CreateRoom;

[Collection(nameof(TestContextFixture))]
public class CreateRoomCommandHandlerTests : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public CreateRoomCommandHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new CreateRoomRequest();

        var command = new CreateRoomCommand(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(command));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsCreateRoomResponse()
    {
        // Arrange
        var name = "a";

        var request = new CreateRoomRequest { Name = name };

        var command = new CreateRoomCommand(request);

        // Act
        var actual = await _fixture.SendAsync(command);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<CreateRoomResponse>(actual);

        Assert.NotNull(actual.Room);

        Assert.Equal(name, actual.Room.Name);
    }
}