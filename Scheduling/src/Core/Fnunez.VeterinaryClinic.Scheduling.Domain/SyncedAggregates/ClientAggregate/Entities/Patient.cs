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

    public Patient(
        int clientId,
        string name,
        AnimalSex animalSex,
        AnimalType animalType,
        int? preferredDoctorId)
    {
        if (clientId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(clientId)} cannot be zero or negative.",
                nameof(clientId));

        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(
                $"Required input {nameof(name)} was empty.",
                nameof(name));

        if (animalType is null)
            throw new ArgumentNullException(
                nameof(name),
                $"Required input {nameof(name)} was empty.");

        if (preferredDoctorId != null && preferredDoctorId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(preferredDoctorId)} cannot be zero or negative.",
                nameof(preferredDoctorId));

        ClientId = clientId;
        Name = name;
        AnimalSex = animalSex;
        AnimalType = animalType;
        PreferredDoctorId = preferredDoctorId;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(
                $"Required input {nameof(name)} was empty.", nameof(name));

        Name = name;
    }

    public void UpdatePreferredDoctorId(int? preferredDoctorId)
    {
        if (preferredDoctorId != null && preferredDoctorId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(preferredDoctorId)} cannot be zero or negative.",
                nameof(preferredDoctorId));

        PreferredDoctorId = preferredDoctorId;
    }

    public override string ToString()
    {
        return Name.ToString();
    }
}