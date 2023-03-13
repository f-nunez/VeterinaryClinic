using System.Text.Json;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared.Components.AppNotification.Factories;

public abstract class BaseAppNotificationListItemFactory
{
    private AppNotificationDto _appNotification { get; set; }
    private string? _message { get; set; }
    private string? _moduleIcon { get; set; }
    private string? _title { get; set; }
    public string? _url { get; set; }

    public BaseAppNotificationListItemFactory(
        AppNotificationDto appNotification)
    {
        _appNotification = appNotification;
    }

    public AppNotificationListItem CreateAppNotificationListItem(int timezoneOffset)
    {
        string createdOn = _appNotification.CreatedOn
            .ToOffset(TimeSpan.FromMinutes(timezoneOffset))
            .ToString("g");

        return new AppNotificationListItem
        {
            CreatedOn = createdOn,
            Id = _appNotification.Id,
            IsRead = _appNotification.IsRead,
            Message = _message,
            ModuleIcon = _moduleIcon,
            Title = _title,
            Url = _url
        };
    }

    protected T? GetPayload<T>()
        => JsonSerializer.Deserialize<T>(
            _appNotification.Payload ?? string.Empty);

    protected void SetMessage(string? messsage) => _message = messsage;

    protected void SetModuleIcon(string? moduleIcon) => _moduleIcon = moduleIcon;

    protected void SetTitle(string? title) => _title = title;

    protected void SetUrl(string? url) => _url = url;
}