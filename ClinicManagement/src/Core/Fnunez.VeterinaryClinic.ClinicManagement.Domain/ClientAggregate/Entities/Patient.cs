using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Enums;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;

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
        ClientId = clientId;
        Name = name;
        AnimalSex = animalSex;
        AnimalType = animalType;
        PreferredDoctorId = preferredDoctorId;
    }

    public override string ToString()
    {
        return Name.ToString();
    }
}