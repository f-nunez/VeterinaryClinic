namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.NotificationCenter;

public class NotificationCenterItemVm
{
    public string? CreatedOn { get; set; }
    public Guid Id { get; set; }
    public bool IsRead { get; set; }
    public string? Message { get; set; }
    public string? ModuleIcon { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
}