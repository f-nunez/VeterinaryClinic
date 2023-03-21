namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Payloads;

public class AppointmentDeletedPayload
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
}