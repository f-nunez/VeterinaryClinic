using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;

public class Client : BaseAuditableEntity<int>, IAggregateRoot
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

        if (preferredDoctorId != null && preferredDoctorId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(preferredDoctorId)} cannot be zero or negative.",
                nameof(preferredDoctorId));

        FullName = fullName;
        PreferredName = preferredName;
        Salutation = salutation;
        EmailAddress = emailAddress;
        PreferredDoctorId = preferredDoctorId;
    }

    public Client(
        int id,
        string fullName,
        string preferredName,
        string salutation,
        string emailAddress,
        int? preferredDoctorId)
    {
        if (id <= 0)
            throw new ArgumentException(
                $"Required input {nameof(id)} cannot be zero or negative.",
                nameof(id));

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

        if (preferredDoctorId != null && preferredDoctorId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(preferredDoctorId)} cannot be zero or negative.",
                nameof(preferredDoctorId));

        FullName = fullName;
        PreferredName = preferredName;
        Salutation = salutation;
        EmailAddress = emailAddress;
        PreferredDoctorId = preferredDoctorId;
    }

    public void AddPatient(Patient patient)
    {
        if (patient is null)
            throw new ArgumentNullException(nameof(patient));

        _patients.Add(patient);
    }

    public void RemovePatient(Patient patient)
    {
        if (patient is null)
            throw new ArgumentNullException(nameof(patient));

        Patient? foundPatient = _patients
            .FirstOrDefault(p => p.Id == patient.Id);

        if (foundPatient is null)
            throw new ArgumentNullException(nameof(patient));

        foundPatient.IsActive = false;
    }

    public void UpdateEmailAddress(string emailAddress)
    {
        if (string.IsNullOrEmpty(emailAddress))
            throw new ArgumentException(
                $"Required input {nameof(emailAddress)} was empty.",
                nameof(emailAddress));

        EmailAddress = emailAddress;
    }

    public void UpdateFullName(string fullName)
    {
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentException(
                $"Required input {nameof(fullName)} was empty.",
                nameof(fullName));

        FullName = fullName;
    }

    public void UpdatePreferredDoctorId(int? preferredDoctorId)
    {
        if (preferredDoctorId != null && preferredDoctorId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(preferredDoctorId)} cannot be zero or negative.",
                nameof(preferredDoctorId));

        PreferredDoctorId = preferredDoctorId;
    }

    public void UpdatePreferredName(string preferredName)
    {
        if (string.IsNullOrEmpty(preferredName))
            throw new ArgumentException(
                $"Required input {nameof(preferredName)} was empty.",
                nameof(preferredName));

        PreferredName = preferredName;
    }

    public void UpdateSalutation(string salutation)
    {
        if (string.IsNullOrEmpty(salutation))
            throw new ArgumentException(
                $"Required input {nameof(salutation)} was empty.",
                nameof(salutation));

        Salutation = salutation;
    }

    public override string ToString()
    {
        return FullName.ToString();
    }
}