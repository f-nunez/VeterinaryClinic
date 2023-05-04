using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;

public class CreateClientRequest : BaseRequest
{
    public string FullName { get; set; } = string.Empty;
    public string PreferredName { get; set; } = string.Empty;
    public string Salutation { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public int? PreferredDoctorId { get; set; }
    public int PreferredLanguage { get; set; }
}