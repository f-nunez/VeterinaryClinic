using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.UnitTests.SyncedAggregates.ClientAggregate;

public class ClientTests
{
    private readonly string _emailAddress = "a@a.com";
    private readonly string _fullName = "a";
    private readonly int _preferredDoctorId = 1;
    private readonly PreferredLanguage _preferredLanguage = PreferredLanguage.English;
    private readonly string _preferredName = "a";
    private readonly string _salutation = "a";

    [Fact]
    public void Constructor_EmailAddress_SetsEmailAddressProperty()
    {
        // Arrange
        var client = new Client(
            _fullName,
            _preferredName,
            _salutation,
            _emailAddress,
            _preferredLanguage,
            _preferredDoctorId
        );

        // Assert
        Assert.Equal(_emailAddress, client.EmailAddress);
    }

    [Fact]
    public void Constructor_FullName_SetsFullNameProperty()
    {
        // Arrange
        var client = new Client(
            _fullName,
            _preferredName,
            _salutation,
            _emailAddress,
            _preferredLanguage,
            _preferredDoctorId
        );

        // Assert
        Assert.Equal(_fullName, client.FullName);
    }

    [Fact]
    public void Constructor_PreferredDoctorId_SetsPreferredDoctorIdProperty()
    {
        // Arrange
        var client = new Client(
            _fullName,
            _preferredName,
            _salutation,
            _emailAddress,
            _preferredLanguage,
            _preferredDoctorId
        );

        // Assert
        Assert.Equal(_preferredDoctorId, client.PreferredDoctorId);
    }

    [Fact]
    public void Constructor_PreferredDoctorIdIsNull_SetsPreferredDoctorIdProperty()
    {
        // Arrange
        int? preferredDoctorId = null;

        var client = new Client(
            _fullName,
            _preferredName,
            _salutation,
            _emailAddress,
            _preferredLanguage,
            preferredDoctorId
        );

        // Assert
        Assert.Equal(preferredDoctorId, client.PreferredDoctorId);
    }

    [Fact]
    public void Constructor_PreferredLanguage_SetsPreferredLanguageProperty()
    {
        // Arrange
        var client = new Client(
            _fullName,
            _preferredName,
            _salutation,
            _emailAddress,
            _preferredLanguage,
            _preferredDoctorId
        );

        // Assert
        Assert.Equal(_preferredLanguage, client.PreferredLanguage);
    }

    [Fact]
    public void Constructor_PreferredName_SetsPreferredNameProperty()
    {
        // Arrange
        var client = new Client(
            _fullName,
            _preferredName,
            _salutation,
            _emailAddress,
            _preferredLanguage,
            _preferredDoctorId
        );

        // Assert
        Assert.Equal(_preferredName, client.PreferredName);
    }

    [Fact]
    public void Constructor_Salutation_SetsSalutationProperty()
    {
        // Arrange
        var client = new Client(
            _fullName,
            _preferredName,
            _salutation,
            _emailAddress,
            _preferredLanguage,
            _preferredDoctorId
        );

        // Assert
        Assert.Equal(_salutation, client.Salutation);
    }
}