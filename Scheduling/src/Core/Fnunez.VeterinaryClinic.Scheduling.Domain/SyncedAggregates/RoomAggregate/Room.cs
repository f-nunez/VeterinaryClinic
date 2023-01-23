using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;

public class Room : BaseEntity<int>, IAggregateRoot
{
    public string Name { get; private set; }

    public Room()
    {
        Name = string.Empty;
    }

    public override string ToString()
    {
        return Name.ToString();
    }
}