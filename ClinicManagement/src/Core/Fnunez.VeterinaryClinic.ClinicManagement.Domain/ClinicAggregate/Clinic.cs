using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

public class Clinic : BaseEntity<int>, IAggregateRoot
{
    public string Address { get; set; }
    public string EmailAddress { get; set; }
    public string Name { get; set; }

    public Clinic()
    {
        Address = string.Empty;
        EmailAddress = string.Empty;
        Name = string.Empty;
    }

    public Clinic(string address, string emailAddress, string name)
    {
        if (string.IsNullOrEmpty(address))
            throw new ArgumentException(
                $"Required input {nameof(address)} was empty.",
                nameof(address));

        if (string.IsNullOrEmpty(emailAddress))
            throw new ArgumentException(
                $"Required input {nameof(emailAddress)} was empty.",
                nameof(emailAddress));

        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(
                $"Required input {nameof(name)} was empty.",
                nameof(name));

        Address = address;
        EmailAddress = emailAddress;
        Name = name;
    }

    public Clinic(int id, string address, string emailAddress, string name)
    {
        if (id <= 0)
            throw new ArgumentException(
                $"Required input {nameof(id)} cannot be zero or negative.",
                nameof(id));

        if (string.IsNullOrEmpty(address))
            throw new ArgumentException(
                $"Required input {nameof(address)} was empty.",
                nameof(address));

        if (string.IsNullOrEmpty(emailAddress))
            throw new ArgumentException(
                $"Required input {nameof(emailAddress)} was empty.",
                nameof(emailAddress));

        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(
                $"Required input {nameof(name)} was empty.",
                nameof(name));

        Id = id;
        Address = address;
        EmailAddress = emailAddress;
        Name = name;
    }
}