using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.UnitTests.ClientAggregate;

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

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_EmailAddressIsEmpty_ThrowsArgumentException(
        string emailAddress)
    {
        // Act
        Action actual = () => new Client(
            _fullName,
            _preferredName,
            _salutation,
            emailAddress,
            _preferredLanguage,
            _preferredDoctorId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
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

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_FullNameIsEmpty_ThrowsArgumentException(
        string fullName)
    {
        // Act
        Action actual = () => new Client(
            fullName,
            _preferredName,
            _salutation,
            _emailAddress,
            _preferredLanguage,
            _preferredDoctorId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
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

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_PreferredDoctorIdIsLessOrEqualsThanZero_ThrowsArgumentException(
        int preferredDoctorId)
    {
        // Act
        Action actual = () => new Client(
            _fullName,
            _preferredName,
            _salutation,
            _emailAddress,
            _preferredLanguage,
            preferredDoctorId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
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

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_PreferredNameIsEmpty_ThrowsArgumentException(
        string preferredName)
    {
        // Act
        Action actual = () => new Client(
            _fullName,
            preferredName,
            _salutation,
            _emailAddress,
            _preferredLanguage,
            _preferredDoctorId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
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

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_SalutationIsEmpty_ThrowsArgumentException(
        string salutation)
    {
        // Act
        Action actual = () => new Client(
            _fullName,
            _preferredName,
            salutation,
            _emailAddress,
            _preferredLanguage,
            _preferredDoctorId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void AddPatient_Patient_AddsIntoPatientsCollection()
    {
        // Arrange
        var patient = new Patient();

        var client = new Client();

        // Act
        client.AddPatient(patient);

        // Assert
        Assert.Contains(patient, client.Patients);
    }

    [Fact]
    public void AddPatient_PatientIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        Patient? patient = null;

        var client = new Client();

        // Act
        Action actual = () => client.AddPatient(patient!);

        // Assert
        Assert.Throws<ArgumentNullException>(actual);
    }

    [Fact]
    public void RemovePatient_Patient_RemovesFromPatientsCollection()
    {
        // Arrange
        var patient = new Patient();

        var client = new Client();

        client.AddPatient(patient);

        // Act
        client.RemovePatient(patient);

        // Assert
        var removedPatient = client.Patients.FirstOrDefault();

        Assert.True(removedPatient?.IsActive == false);
    }

    [Fact]
    public void RemovePatient_PatientIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        Patient? patient = new Patient();

        var client = new Client();

        client.AddPatient(patient);

        patient = null;

        // Act
        Action actual = () => client.RemovePatient(patient!);

        // Assert
        Assert.Throws<ArgumentNullException>(actual);
    }

    [Fact]
    public void UpdateEmailAddress_EmailAddress_UpdatesEmailAddressProperty()
    {
        // Arrange
        var client = new Client();

        // Act
        client.UpdateEmailAddress(_emailAddress);

        // Assert
        Assert.Equal(_emailAddress, client.EmailAddress);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateEmailAddress_EmailAddressIsEmpty_ThrowsArgumentException(
        string emailAddress)
    {
        // Arrange
        var client = new Client();

        // Act
        Action actual = () => client.UpdateEmailAddress(emailAddress);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateFullName_FullName_UpdatesFullNameProperty()
    {
        // Arrange
        var client = new Client();

        // Act
        client.UpdateFullName(_fullName);

        // Assert
        Assert.Equal(_fullName, client.FullName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateFullName_FullNameIsEmpty_ThrowsArgumentException(
        string fullName)
    {
        // Arrange
        var client = new Client();

        // Act
        Action actual = () => client.UpdateFullName(fullName);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdatePreferredDoctorId_PreferredDoctorId_UpdatesPreferredDoctorIdProperty()
    {
        // Arrange
        var client = new Client();

        // Act
        client.UpdatePreferredDoctorId(_preferredDoctorId);

        // Assert
        Assert.Equal(_preferredDoctorId, client.PreferredDoctorId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void UpdatePreferredDoctorId_PreferredDoctorIdIsLessOrEqualsThanZero_ThrowsArgumentException(
        int preferredDoctorId)
    {
        // Arrange
        var client = new Client();

        // Act
        Action actual = () => client
            .UpdatePreferredDoctorId(preferredDoctorId);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdatePreferredDoctorId_PreferredDoctorIdIsNull_UpdatesPreferredDoctorIdProperty()
    {
        // Arrange
        int? preferredDoctorId = null;

        var client = new Client();

        // Act
        client.UpdatePreferredDoctorId(preferredDoctorId);

        // Assert
        Assert.Equal(preferredDoctorId, client.PreferredDoctorId);
    }

    [Fact]
    public void UpdatePreferredLanguage_PreferredLanguage_UpdatesPreferredLanguageProperty()
    {
        // Arrange
        var client = new Client();

        // Act
        client.UpdatePreferredLanguage(_preferredLanguage);

        // Assert
        Assert.Equal(_preferredLanguage, client.PreferredLanguage);
    }

    [Fact]
    public void UpdatePreferredName_PreferredName_UpdatesPreferredNameProperty()
    {
        // Arrange
        var client = new Client();

        // Act
        client.UpdatePreferredName(_preferredName);

        // Assert
        Assert.Equal(_preferredName, client.PreferredName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdatePreferredName_PreferredNameIsEmpty_ThrowsArgumentException(
        string preferredName)
    {
        // Arrange
        var client = new Client();

        // Act
        Action actual = () => client.UpdatePreferredName(preferredName);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateSalutation_Salutation_UpdatesSalutationProperty()
    {
        // Arrange
        var client = new Client();

        // Act
        client.UpdateSalutation(_salutation);

        // Assert
        Assert.Equal(_salutation, client.Salutation);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateSalutation_SalutationIsEmpty_ThrowsArgumentException(
        string salutation)
    {
        // Arrange
        var client = new Client();

        // Act
        Action actual = () => client.UpdateSalutation(salutation);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }
}