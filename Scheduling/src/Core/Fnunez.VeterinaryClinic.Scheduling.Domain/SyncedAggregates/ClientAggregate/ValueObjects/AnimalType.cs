using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.ValueObjects;

public class AnimalType : ValueObject
{
    public string Breed { get; private set; } = string.Empty;
    public string Species { get; private set; } = string.Empty;

    public AnimalType()
    {
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Breed;
        yield return Species;
    }
}