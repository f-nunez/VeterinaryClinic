using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.UnitTests.ClientAggregate.ValueObjects;

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

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_NameIsEmpty_ThrowsArgumentException(string name)
    {
        // Act
        Action actual = () => new Photo(name, _storedName);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_StoredName_SetsStoredNameProperty()
    {
        // Arrange
        var photo = new Photo(_name, _storedName);

        // Assert
        Assert.Equal(_storedName, photo.StoredName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_StoredNameIsEmpty_ThrowsArgumentException(
        string storedName)
    {
        // Act
        Action actual = () => new Photo(_name, storedName);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }
}