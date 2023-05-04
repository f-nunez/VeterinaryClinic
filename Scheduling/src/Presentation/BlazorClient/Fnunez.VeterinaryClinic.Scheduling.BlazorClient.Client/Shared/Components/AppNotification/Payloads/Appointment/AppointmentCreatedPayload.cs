namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Payloads;

public class AppointmentCreatedPayload
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
}