using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;

public class AppointmentType : BaseEntity<int>, IAggregateRoot
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public int Duration { get; private set; }

    public AppointmentType()
    {
        Name = string.Empty;
        Code = string.Empty;
    }

    public AppointmentType(
        string name,
        string code,
        int duration)
    {
        Name = name;
        Code = code;
        Duration = duration;
    }

    public AppointmentType(
        int id,
        string name,
        string code,
        int duration)
    {
        Id = id;
        Name = name;
        Code = code;
        Duration = duration;
    }

    public override string ToString()
    {
        return Name;
    }
}