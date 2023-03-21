namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Payloads;

public class PatientCreatedPayload
{
    public int ClientId { get; set; }
    public int PatientId { get; set; }
    public string? Name { get; set; }
}