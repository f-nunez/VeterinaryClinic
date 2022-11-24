using Fnunez.VeterinaryClinic.ClinicManagement.Domain.Entities;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.Aggregates;

public class Client : BaseEntity<int>, IAggregateRoot
{
    public string FullName { get; private set; }
    public string PreferredName { get; private set; }
    public string Salutation { get; private set; }
    public string EmailAddress { get; private set; }
    public int PreferredDoctorId { get; private set; }
    public IList<Patient> Patients { get; private set; } = new List<Patient>();

    public Client()
    {
        FullName = string.Empty;
        PreferredName = string.Empty;
        Salutation = string.Empty;
        EmailAddress = string.Empty;
    }

    public Client(
        string fullName,
        string preferredName,
        string salutation,
        string emailAddress,
        int preferredDoctorId)
    {
        FullName = fullName;
        PreferredName = preferredName;
        Salutation = salutation;
        EmailAddress = emailAddress;
        PreferredDoctorId = preferredDoctorId;
    }

    public override string ToString()
    {
        return FullName.ToString();
    }
}