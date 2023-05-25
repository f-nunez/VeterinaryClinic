using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.ApplicationRoleAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.UnitTests.ApplicationRoleAggregate;

public class ApplicationRoleTests
{
    [Fact]
    public void Constructor_Id_SetsIdProperty()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();

        var name = "role name";

        var applicationRole = new ApplicationRole
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
        var name = "role name";

        // Act
        Action actual = () => new ApplicationRole(id, name);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_Name_SetsNameProperty()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();

        var name = "role name";

        var applicationRole = new ApplicationRole
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
        Action actual = () => new ApplicationRole(id, name);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }
}