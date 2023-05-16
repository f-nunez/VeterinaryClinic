using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.UnitTests.SyncedAggregates.ClientAggregate.ValueObjects;

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

    [Fact]
    public void Constructor_Species_SetsSpeciesProperty()
    {
        // Arrange
        var animalType = new AnimalType(_breed, _species);

        // Assert
        Assert.Equal(_species, animalType.Species);
    }
}