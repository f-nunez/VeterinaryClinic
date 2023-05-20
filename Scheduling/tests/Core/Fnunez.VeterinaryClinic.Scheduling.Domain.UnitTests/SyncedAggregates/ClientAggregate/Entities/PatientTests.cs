using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.UnitTests.SyncedAggregates.ClientAggregate.Entities;

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
}