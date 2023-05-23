namespace Fnunez.VeterinaryClinic.Scheduling.Application.Services.IntegrationEventReceiver.IntegrationEvents;

public class ClientCreatedIntegrationEvent : BaseIntegrationEvent
{
    public string ClientEmailAddress { get; set; } = string.Empty;
    public string ClientFullName { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public int? ClientPreferredDoctorId { get; set; }
    public int ClientPreferredLanguage { get; set; }
    public string ClientPreferredName { get; set; } = string.Empty;
    public string ClientSalutation { get; set; } = string.Empty;
}