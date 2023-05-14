using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRoomsFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomsFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.IntegrationTests.Features.Rooms.Queries.GetRoomsFilterName;

[Collection(nameof(TestContextFixture))]
public class GetRoomsFilterNameQueryHandlerTests
    : IClassFixture<TestContextFixture>
{
    private readonly TestContextFixture _fixture;

    public GetRoomsFilterNameQueryHandlerTests(TestContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_RequiredEntriesAreEmpty_ThrowsValidationException()
    {
        // Arrange
        var request = new GetRoomsFilterNameRequest();

        var query = new GetRoomsFilterNameQuery(request);

        // Act
        var actual = await Assert.ThrowsAsync<ValidationException>(() =>
            _fixture.SendAsync(query));

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<ValidationException>(actual);
    }

    [Fact]
    public async Task Handle_ReturnsGetRoomsFilterNameResponse()
    {
        // Arrange
        var name = "a";

        var room = new Room(name);

        await _fixture.AddAsync<Room>(room);

        var request = new GetRoomsFilterNameRequest
        {
            NameFilterValue = name
        };

        var query = new GetRoomsFilterNameQuery(request);

        // Act
        var actual = await _fixture.SendAsync(query);

        // Assert
        Assert.NotNull(actual);

        Assert.IsType<GetRoomsFilterNameResponse>(actual);

        Assert.NotNull(actual.RoomNames);

        Assert.NotEmpty(actual.RoomNames);

        Assert.StartsWith(name, actual.RoomNames.FirstOrDefault());
    }
}