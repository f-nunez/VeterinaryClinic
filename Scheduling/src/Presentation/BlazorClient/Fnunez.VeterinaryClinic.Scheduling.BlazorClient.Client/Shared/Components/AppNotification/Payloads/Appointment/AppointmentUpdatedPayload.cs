namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared.Components.AppNotification.Payloads;

public class AppointmentUpdatedPayload
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
}