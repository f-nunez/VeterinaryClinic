using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.CreateClinic;

public class CreateClinicRequest : BaseRequest
{
    public string Address { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}