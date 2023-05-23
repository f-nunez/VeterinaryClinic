using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;

public class Doctor : BaseEntity<int>, IAggregateRoot
{
    public string FullName { get; private set; }

    public Doctor()
    {
        FullName = string.Empty;
    }

    public Doctor(string fullName)
    {
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentException(
                $"Required input {nameof(fullName)} was empty.",
                nameof(fullName));

        FullName = fullName;
    }

    public Doctor(int id, string fullName)
    {
        if (id <= 0)
            throw new ArgumentException(
                $"Required input {nameof(id)} cannot be zero or negative.",
                nameof(id));

        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentException(
                $"Required input {nameof(fullName)} was empty.",
                nameof(fullName));

        Id = id;
        FullName = fullName;
    }

    public void UpdateFullName(string fullName)
    {
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentException(
                $"Required input {nameof(fullName)} was empty.",
                nameof(fullName));

        FullName = fullName;
    }

    public override string ToString()
    {
        return FullName.ToString();
    }
}