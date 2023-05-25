using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.UnitTests.ClinicAggregate;

public class ClinicTests
{
    private readonly string _address = "a";
    private readonly string _emailAddress = "a";
    private readonly string _name = "a";

    [Fact]
    public void Constructor_Address_SetsAddressProperty()
    {
        // Arrange
        var clinic = new Clinic(_address, _emailAddress, _name);

        // Assert
        Assert.Equal(_address, clinic.Address);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_AddressIsEmpty_ThrowsArgumentException(
        string address)
    {
        // Act
        Action actual = () => new Clinic(address, _emailAddress, _name);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_EmailAddress_SetsEmailAddressProperty()
    {
        // Arrange
        var clinic = new Clinic(_address, _emailAddress, _name);

        // Assert
        Assert.Equal(_emailAddress, clinic.EmailAddress);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_EmailAddressIsEmpty_ThrowsArgumentException(
        string emailAddress)
    {
        // Act
        Action actual = () => new Clinic(_address, emailAddress, _name);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_Name_SetsNameProperty()
    {
        // Arrange
        var clinic = new Clinic(_address, _emailAddress, _name);

        // Assert
        Assert.Equal(_name, clinic.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_NameIsEmpty_ThrowsArgumentException(string name)
    {
        // Act
        Action actual = () => new Clinic(_address, _emailAddress, name);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateAddress_Address_UpdatesAddressProperty()
    {
        // Arrange
        var clinic = new Clinic();

        // Act
        clinic.UpdateAddress(_address);

        // Assert
        Assert.Equal(_address, clinic.Address);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateAddress_AddressIsEmpty_ThrowsArgumentException(
        string address)
    {
        // Arrange
        var clinic = new Clinic();

        // Act
        Action actual = () => clinic.UpdateAddress(address);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateEmailAddress_EmailAddress_UpdatesEmailAddressProperty()
    {
        // Arrange
        var clinic = new Clinic();

        // Act
        clinic.UpdateEmailAddress(_emailAddress);

        // Assert
        Assert.Equal(_emailAddress, clinic.EmailAddress);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateEmailAddress_EmailAddressIsEmpty_ThrowsArgumentException(
        string emailAddress)
    {
        // Arrange
        var clinic = new Clinic();

        // Act
        Action actual = () => clinic.UpdateEmailAddress(emailAddress);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateName_Name_UpdatesNameProperty()
    {
        // Arrange
        var clinic = new Clinic();

        // Act
        clinic.UpdateName(_name);

        // Assert
        Assert.Equal(_name, clinic.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateName_NameIsEmpty_ThrowsArgumentException(string name)
    {
        // Arrange
        var clinic = new Clinic();

        // Act
        Action actual = () => clinic.UpdateName(name);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }
}