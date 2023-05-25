using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.UnitTests.ClientAggregate.ValueObjects;

public class AnimalTypeTests
{
    private readonly string _breed = "a";
    private readonly string _species = "a";

    [Fact]
    public void Constructor_Breed_SetsBreedProperty()
    {
        // Arrange
        var animalType = new AnimalType(_breed, _species);

        // Assert
        Assert.Equal(_breed, animalType.Breed);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_BreedIsEmpty_ThrowsArgumentException(string breed)
    {
        // Act
        Action actual = () => new AnimalType(breed, _species);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_Species_SetsSpeciesProperty()
    {
        // Arrange
        var animalType = new AnimalType(_breed, _species);

        // Assert
        Assert.Equal(_species, animalType.Species);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_SpeciesIsEmpty_ThrowsArgumentException(
        string species)
    {
        // Act
        Action actual = () => new AnimalType(_breed, species);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }
}