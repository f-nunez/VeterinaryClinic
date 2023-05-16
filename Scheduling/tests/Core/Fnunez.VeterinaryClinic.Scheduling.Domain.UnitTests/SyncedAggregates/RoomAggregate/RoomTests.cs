using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.UnitTests.SyncedAggregates.RoomAggregate;

public class RoomTests
{
    private readonly string _name = "a";

    [Fact]
    public void Constructor_Name_SetsNameProperty()
    {
        // Arrange
        var room = new Room(_name);

        // Assert
        Assert.Equal(_name, room.Name);
    }
}