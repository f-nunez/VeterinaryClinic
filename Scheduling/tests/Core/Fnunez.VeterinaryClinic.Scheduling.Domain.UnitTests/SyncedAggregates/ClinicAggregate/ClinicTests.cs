using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.UnitTests.SyncedAggregates.ClinicAggregate;

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

    [Fact]
    public void Constructor_EmailAddress_SetsEmailAddressProperty()
    {
        // Arrange
        var clinic = new Clinic(_address, _emailAddress, _name);

        // Assert
        Assert.Equal(_emailAddress, clinic.EmailAddress);
    }

    [Fact]
    public void Constructor_Name_SetsNameProperty()
    {
        // Arrange
        var clinic = new Clinic(_address, _emailAddress, _name);

        // Assert
        Assert.Equal(_name, clinic.Name);
    }
}