using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

public class Room : BaseAuditableEntity<int>, IAggregateRoot
{
    public string Name { get; private set; }

    public Room()
    {
        Name = string.Empty;
    }

    public Room(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(
                $"Required input {nameof(name)} was empty.",
                nameof(name));

        Name = name;
    }

    public Room(int id, string name)
    {
        if (id <= 0)
            throw new ArgumentException(
                $"Required input {nameof(id)} cannot be zero or negative.",
                nameof(id));

        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(
                $"Required input {nameof(name)} was empty.",
                nameof(name));

        Id = id;
        Name = name;
    }

    public override string ToString()
    {
        return Name.ToString();
    }
}