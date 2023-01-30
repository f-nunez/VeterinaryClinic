namespace Contracts;

public class ClientCreatedIntegrationEventContract
    : BaseIntegrationEventContract
{
    public string ClientEmailAddress { get; set; } = string.Empty;
    public string ClientFullName { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public int? ClientPreferredDoctorId { get; set; }
    public string ClientPreferredName { get; set; } = string.Empty;
    public string ClientSalutation { get; set; } = string.Empty;
}