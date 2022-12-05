using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;

public class AnimalType : ValueObject
{
    public string Breed { get; private set; } = string.Empty;
    public string Species { get; private set; } = string.Empty;

    public AnimalType()
    {
    }

    public AnimalType(string breed, string species)
    {
        Breed = breed;
        Species = species;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Breed;
        yield return Species;
    }
}