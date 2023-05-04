using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;

public class Patient : BaseEntity<int>
{
    public int ClientId { get; private set; }
    public string Name { get; private set; }
    public AnimalSex AnimalSex { get; private set; }
    public AnimalType AnimalType { get; private set; } = null!;
    public Photo Photo { get; set; } = null!;
    public int? PreferredDoctorId { get; private set; }

    public Patient()
    {
        Name = string.Empty;
    }

    public Patient(
        int clientId,
        string name,
        AnimalSex animalSex,
        AnimalType animalType,
        Photo photo,
        int? preferredDoctorId)
    {
        ClientId = clientId;
        Name = name;
        AnimalSex = animalSex;
        AnimalType = animalType;
        Photo = photo;
        PreferredDoctorId = preferredDoctorId;
    }

    public override string ToString()
    {
        return Name.ToString();
    }
}