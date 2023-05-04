using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.ValueObjects;

public class Photo : ValueObject
{
    public string Name { get; set; }
    public string StoredName { get; set; }

    public Photo()
    {
        Name = string.Empty;
        StoredName = string.Empty;
    }

    public Photo(string name, string storedName)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException(
                $"Required input {nameof(name)} was empty.",
                nameof(name));

        if (string.IsNullOrEmpty(storedName))
            throw new ArgumentException(
                $"Required input {nameof(storedName)} was empty.",
                nameof(storedName));

        Name = name;
        StoredName = storedName;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return StoredName;
    }
}