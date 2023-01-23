using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.ValueObjects;

public class Photo : ValueObject
{
    public string Name { get; set; }
    public string StoredName { get; set; }

    public Photo()
    {
        Name = string.Empty;
        StoredName = string.Empty;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return StoredName;
    }
}