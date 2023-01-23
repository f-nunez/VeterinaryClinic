using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;

public class Patient : BaseEntity<int>
{
    public int ClientId { get; private set; }
    public string Name { get; private set; }
    public AnimalSex AnimalSex { get; private set; }
    public AnimalType? AnimalType { get; private set; }
    public int? PreferredDoctorId { get; private set; }

    public Patient()
    {
        Name = string.Empty;
    }

    public override string ToString()
    {
        return Name.ToString();
    }
}