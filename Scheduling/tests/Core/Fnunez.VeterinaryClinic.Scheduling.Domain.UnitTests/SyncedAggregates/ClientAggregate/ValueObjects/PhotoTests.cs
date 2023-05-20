using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.UnitTests.SyncedAggregates.ClientAggregate.ValueObjects;

public class PhotoTests
{
    private readonly string _name = "a";
    private readonly string _storedName = "a";

    [Fact]
    public void Constructor_Name_SetsNameProperty()
    {
        // Arrange
        var photo = new Photo(_name, _storedName);

        // Assert
        Assert.Equal(_name, photo.Name);
    }

    [Fact]
    public void Constructor_StoredName_SetsStoredNameProperty()
    {
        // Arrange
        var photo = new Photo(_name, _storedName);

        // Assert
        Assert.Equal(_storedName, photo.StoredName);
    }
}