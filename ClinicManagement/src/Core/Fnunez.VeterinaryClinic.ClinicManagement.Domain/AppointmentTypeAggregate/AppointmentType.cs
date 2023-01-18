using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

public class AppointmentType : BaseEntity<int>, IAggregateRoot
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public int Duration { get; private set; }

    public AppointmentType()
    {
        Name = string.Empty;
        Code = string.Empty;
    }

    public AppointmentType(
        string name,
        string code,
        int duration)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(
                $"Required input {nameof(name)} was empty.",
                nameof(name));

        if (string.IsNullOrEmpty(code))
            throw new ArgumentException(
                $"Required input {nameof(code)} was empty.",
                nameof(code));

        if (duration <= 0)
            throw new ArgumentException(
                $"Required input {nameof(duration)} cannot be zero or negative.",
                nameof(duration));

        Name = name;
        Code = code;
        Duration = duration;
    }

    public AppointmentType(
        int id,
        string name,
        string code,
        int duration)
    {
        if (id <= 0)
            throw new ArgumentException(
                $"Required input {nameof(id)} cannot be zero or negative.",
                nameof(id));

        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(
                $"Required input {nameof(name)} was empty.",
                nameof(name));

        if (string.IsNullOrEmpty(code))
            throw new ArgumentException(
                $"Required input {nameof(code)} was empty.",
                nameof(code));

        if (duration <= 0)
            throw new ArgumentException(
                $"Required input {nameof(duration)} cannot be zero or negative.",
                nameof(duration));

        Id = id;
        Name = name;
        Code = code;
        Duration = duration;
    }

    public override string ToString()
    {
        return Name;
    }
}