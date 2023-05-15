using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.UnitTests.ClientAggregate.Entities;

public class PatientTests
{
    private readonly AnimalSex _animalSex = AnimalSex.Female;
    private readonly AnimalType _animalType = new AnimalType("a", "a");
    private readonly int _clientId = 1;
    private readonly string _name = "a";
    private readonly Photo _photo = new Photo("a", "a");
    private readonly int _preferredDoctorId = 1;

    [Fact]
    public void Constructor_AnimalSex_SetsAnimalSexProperty()
    {
        // Arrange
        var patient = new Patient(
            _clientId,
            _name,
            _animalSex,
            _animalType,
            _photo,
            _preferredDoctorId
        );

        // Assert
        Assert.Equal(_animalSex, patient.AnimalSex);
    }

    [Fact]
    public void Constructor_AnimalType_SetsAnimalTypeProperty()
    {
        // Arrange
        var patient = new Patient(
            _clientId,
            _name,
            _animalSex,
            _animalType,
            _photo,
            _preferredDoctorId
        );

        // Assert
        Assert.Equal(_animalType, patient.AnimalType);
    }

    [Fact]
    public void Constructor_AnimalTypeIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        AnimalType? animalType = null;

        // Act
        Action actual = () => new Patient(
            _clientId,
            _name,
            _animalSex,
            animalType!,
            _photo,
            _preferredDoctorId
        );

        // Assert
        Assert.Throws<ArgumentNullException>(actual);
    }

    [Fact]
    public void Constructor_ClientId_SetsClientIdProperty()
    {
        // Arrange
        var patient = new Patient(
            _clientId,
            _name,
            _animalSex,
            _animalType,
            _photo,
            _preferredDoctorId
        );

        // Assert
        Assert.Equal(_clientId, patient.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ClientIdIsLessThanOrEqualToZero_ThrowsArgumentException(
        int clientId)
    {
        // Act
        Action actual = () => new Patient(
            clientId,
            _name,
            _animalSex,
            _animalType,
            _photo,
            _preferredDoctorId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_Name_SetsNameProperty()
    {
        // Arrange
        var patient = new Patient(
            _clientId,
            _name,
            _animalSex,
            _animalType,
            _photo,
            _preferredDoctorId
        );

        // Assert
        Assert.Equal(_name, patient.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_NameIsEmpty_ThrowsArgumentException(string name)
    {
        // Act
        Action actual = () => new Patient(
            _clientId,
            name,
            _animalSex,
            _animalType,
            _photo,
            _preferredDoctorId
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_Photo_SetsPhotoProperty()
    {
        // Arrange
        var patient = new Patient(
            _clientId,
            _name,
            _animalSex,
            _animalType,
            _photo,
            _preferredDoctorId
        );

        // Assert
        Assert.Equal(_photo, patient.Photo);
    }

    [Fact]
    public void Constructor_PhotoIsEmpty_ThrowsArgumentNullException()
    {
        // Arrange
        Photo? photo = null;

        // Act
        Action actual = () => new Patient(
            _clientId,
            _name,
            _animalSex,
            _animalType,
            photo!,
            _preferredDoctorId
        );

        // Assert
        Assert.Throws<ArgumentNullException>(actual);
    }

    [Fact]
    public void Constructor_PreferredDoctorId_SetsPreferredDoctorIdProperty()
    {
        // Arrange
        var patient = new Patient(
            _clientId,
            _name,
            _animalSex,
            _animalType,
            _photo,
            _preferredDoctorId
        );

        // Assert
        Assert.Equal(_clientId, patient.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_PreferredDoctorIdIsLessThanOrEqualToZero_ThrowsArgumentException(
        int preferredDoctorId)
    {
        // Act
        Action actual = () => new Patient(
            _clientId,
            _name,
            _animalSex,
            _animalType,
            _photo,
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

        var patient = new Patient(
            _clientId,
            _name,
            _animalSex,
            _animalType,
            _photo,
            preferredDoctorId
        );

        // Assert
        Assert.Equal(preferredDoctorId, patient.PreferredDoctorId);
    }

    [Fact]
    public void UpdateAnimalSex_AnimalSex_UpdatesAnimalSexProperty()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.UpdateAnimalSex(_animalSex);

        // Assert
        Assert.Equal(_animalSex, patient.AnimalSex);
    }

    [Fact]
    public void UpdateAnimalType_AnimalType_UpdatesAnimalTypeProperty()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.UpdateAnimalType(_animalType);

        // Assert
        Assert.Equal(_animalType, patient.AnimalType);
    }

    [Fact]
    public void UpdateAnimalType_AnimalTypeIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        AnimalType? animalType = null;

        var patient = new Patient();

        // Act
        Action actual = () => patient.UpdateAnimalType(animalType!);

        // Assert
        Assert.Throws<ArgumentNullException>(actual);
    }

    [Fact]
    public void UpdateName_Name_UpdatesNameProperty()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.UpdateName(_name);

        // Assert
        Assert.Equal(_name, patient.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateName_NameIsEmpty_ThrowsArgumentException(string name)
    {
        // Arrange
        var patient = new Patient();

        // Act
        Action actual = () => patient.UpdateName(name);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdatePhoto_Photo_UpdatesPhotoProperty()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.UpdatePhoto(_photo);

        // Assert
        Assert.Equal(_photo, patient.Photo);
    }

    [Fact]
    public void UpdatePhoto_PhotoIsNull_ThrowsArgumentNullexception()
    {
        // Arrange
        Photo? photo = null;

        var patient = new Patient();

        // Act
        Action actual = () => patient.UpdatePhoto(photo!);

        // Assert
        Assert.Throws<ArgumentNullException>(actual);
    }

    [Fact]
    public void UpdatePreferredDoctorId_PreferredDoctorId_UpdatesPreferredDoctorIdProperty()
    {
        // Arrange
        var patient = new Patient();

        // Act
        patient.UpdatePreferredDoctorId(_preferredDoctorId);

        // Assert
        Assert.Equal(_preferredDoctorId, patient.PreferredDoctorId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void UpdatePreferredDoctorId_PreferredDoctorIdIsLessThanOrEqualToZero_ThrowsArgumentException(
        int preferredDoctorId)
    {
        // Arrange
        var patient = new Patient();

        // Act
        Action actual = () => patient
            .UpdatePreferredDoctorId(preferredDoctorId);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdatePreferredDoctorId_PreferredDoctorIdIsNull_UpdatesPreferredDoctorIdProperty()
    {
        // Arrange
        int? preferredDoctorId = null;

        var patient = new Patient();

        // Act
        patient.UpdatePreferredDoctorId(preferredDoctorId);

        // Assert
        Assert.Equal(preferredDoctorId, patient.PreferredDoctorId);
    }
}