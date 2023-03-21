using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Notifications.Commands.DeleteAppNotifications;

public class DeleteAppNotificationsRequest : BaseRequest
{
    public List<Guid> AppNotificationIds { get; set; } = new();
}