using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;

public class AnimalType : ValueObject
{
    public string Breed { get; private set; }
    public string Species { get; private set; }

    public AnimalType()
    {
        Breed = string.Empty;
        Species = string.Empty;
    }

    public AnimalType(string breed, string species)
    {
        if (string.IsNullOrEmpty(breed))
            throw new ArgumentException(
                $"Required input {nameof(breed)} was empty.",
                nameof(breed));

        if (string.IsNullOrEmpty(species))
            throw new ArgumentException(
                $"Required input {nameof(species)} was empty.",
                nameof(species));

        Breed = breed;
        Species = species;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Breed;
        yield return Species;
    }
}