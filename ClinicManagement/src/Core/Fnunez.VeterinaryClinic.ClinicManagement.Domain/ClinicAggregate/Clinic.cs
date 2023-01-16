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
        Address = address;
        EmailAddress = emailAddress;
        Name = name;
    }
}