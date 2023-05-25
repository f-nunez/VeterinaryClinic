using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.ApplicationUserAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.UnitTests.ApplicationUserAggregate;

public class ApplicationUserTests
{
    [Fact]
    public void Constructor_Id_SetsIdProperty()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();

        var name = "name";

        var applicationRole = new ApplicationUser
        (
            id,
            name
        );

        // Assert
        Assert.Equal(id, applicationRole.Id);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_IdIsEmpty_ThrowsArgumentException(string id)
    {
        // Arrange
        var name = "name";

        // Act
        Action actual = () => new ApplicationUser(id, name);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_Name_SetsNameProperty()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();

        var name = "name";

        var applicationRole = new ApplicationUser
        (
            id,
            name
        );

        // Assert
        Assert.Equal(name, applicationRole.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_NameIsEmpty_ThrowsArgumentException(string name)
    {
        // Arrange
        var id = Guid.NewGuid().ToString();

        // Act
        Action actual = () => new ApplicationUser(id, name);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }
}