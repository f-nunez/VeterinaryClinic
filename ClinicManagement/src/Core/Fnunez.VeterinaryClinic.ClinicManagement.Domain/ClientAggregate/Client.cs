using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;

public class Client : BaseEntity<int>, IAggregateRoot
{
    private IList<Patient> _patients = new List<Patient>();
    public string FullName { get; private set; }
    public string PreferredName { get; private set; }
    public string Salutation { get; private set; }
    public string EmailAddress { get; private set; }
    public int PreferredDoctorId { get; private set; }
    public IReadOnlyList<Patient> Patients => _patients.AsReadOnly();

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
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentException(
                $"Required input {nameof(fullName)} was empty.",
                nameof(fullName));

        if (string.IsNullOrEmpty(preferredName))
            throw new ArgumentException(
                $"Required input {nameof(preferredName)} was empty.",
                nameof(preferredName));

        if (string.IsNullOrEmpty(salutation))
            throw new ArgumentException(
                $"Required input {nameof(salutation)} was empty.",
                nameof(salutation));

        if (string.IsNullOrEmpty(emailAddress))
            throw new ArgumentException(
                $"Required input {nameof(emailAddress)} was empty.",
                nameof(emailAddress));

        if (preferredDoctorId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(preferredDoctorId)} cannot be zero or negative.",
                nameof(preferredDoctorId));

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