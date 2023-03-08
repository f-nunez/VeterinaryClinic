namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification;

public class AppNotificationListItem
{
    public string? CreatedOn { get; set; }
    public Guid Id { get; set; }
    public bool IsRead { get; set; }
    public string? Message { get; set; }
    public string? ModuleIcon { get; set; }
    public string? Title { get; set; }
}