using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;

public class Client : BaseEntity<int>, IAggregateRoot
{
    private IList<Patient> _patients = new List<Patient>();
    public string FullName { get; private set; }
    public string PreferredName { get; private set; }
    public string Salutation { get; private set; }
    public string EmailAddress { get; private set; }
    public int? PreferredDoctorId { get; private set; }
    public IReadOnlyList<Patient> Patients => _patients.AsReadOnly();

    #region Navigations
    public Doctor PreferredDoctor { get; private set; } = null!;
    #endregion

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
        int? preferredDoctorId)
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