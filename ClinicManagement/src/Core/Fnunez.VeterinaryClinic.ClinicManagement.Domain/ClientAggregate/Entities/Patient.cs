using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;

public class Patient : BaseAuditableEntity<int>
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
                nameof(animalType),
                $"Required input {nameof(animalType)} was empty.");

        if (photo is null)
            throw new ArgumentNullException(
                nameof(photo),
                $"Required input {nameof(photo)} was empty.");

        if (preferredDoctorId != null && preferredDoctorId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(preferredDoctorId)} cannot be zero or negative.",
                nameof(preferredDoctorId));

        ClientId = clientId;
        Name = name;
        AnimalSex = animalSex;
        AnimalType = animalType;
        Photo = photo;
        PreferredDoctorId = preferredDoctorId;
    }

    public Patient(
        int id,
        int clientId,
        string name,
        AnimalSex animalSex,
        AnimalType animalType,
        Photo photo,
        int? preferredDoctorId)
    {
        if (id <= 0)
            throw new ArgumentException(
                $"Required input {nameof(id)} cannot be zero or negative.",
                nameof(id));

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
                nameof(animalType),
                $"Required input {nameof(animalType)} was empty.");

        if (photo is null)
            throw new ArgumentNullException(
                nameof(photo),
                $"Required input {nameof(photo)} was empty.");

        if (preferredDoctorId != null && preferredDoctorId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(preferredDoctorId)} cannot be zero or negative.",
                nameof(preferredDoctorId));

        ClientId = clientId;
        Name = name;
        AnimalSex = animalSex;
        AnimalType = animalType;
        Photo = photo;
        PreferredDoctorId = preferredDoctorId;
    }

    public void UpdateAnimalSex(AnimalSex animalSex)
    {
        AnimalSex = animalSex;
    }

    public void UpdateAnimalType(AnimalType animalType)
    {
        if (animalType is null)
            throw new ArgumentNullException(
                nameof(animalType),
                $"Required input {nameof(animalType)} was empty.");

        AnimalType = animalType;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(
                $"Required input {nameof(name)} was empty.", nameof(name));

        Name = name;
    }

    public void UpdatePhoto(Photo photo)
    {
        if (photo is null)
            throw new ArgumentNullException(
                nameof(photo),
                $"Required input {nameof(photo)} was empty.");

        Photo = photo;
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