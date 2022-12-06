using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;

public class Doctor : BaseEntity<int>, IAggregateRoot
{
    public string FullName { get; private set; }

    public Doctor()
    {
        FullName = string.Empty;
    }

    public Doctor(string fullName)
    {
        FullName = fullName;
    }

    public Doctor(int id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }

    public override string ToString()
    {
        return FullName.ToString();
    }
}