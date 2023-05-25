using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRooms;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRooms;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.IntegrationTests.Features.Rooms.Queries.GetRooms;

[Collection(nameof(TestContextFixture))]
public class GetRoomsQueryHandlerTests : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetRoomsQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetRoomsRequest();

        var query = new GetRoomsQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetRoomsResponse()
    {
        // Arrange
        var name = "a";

        var room = new Room(name);

        await _fixture.AddAsync<Room>(room);

        var take = 1;

        var request = new GetRoomsRequest
        {
            DataGridRequest = new DataGridRequest { Take = take }
        };

        var query = new GetRoomsQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetRoomsResponse>(actual);

        Assert.NotNull(actual.DataGridResponse);

        Assert.NotEmpty(actual.DataGridResponse.Items);

        Assert.Equal(take, actual.DataGridResponse.Items.Count);
    }
}