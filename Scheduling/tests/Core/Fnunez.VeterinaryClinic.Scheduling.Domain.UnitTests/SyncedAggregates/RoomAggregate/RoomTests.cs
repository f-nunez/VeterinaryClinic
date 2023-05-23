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

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_NameIsEmpty_ThrowsArgumentException(string name)
    {
        // Act
        Action actual = () => new Room(name);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateName_Name_UpdatesNameProperty()
    {
        // Arrange
        var room = new Room();

        // Act
        room.UpdateName(_name);

        // Assert
        Assert.Equal(_name, room.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateName_NameIsEmpty_ThrowsArgumentException(string name)
    {
        // Arrange
        var room = new Room();

        // Act
        Action actual = () => room.UpdateName(name);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }
}