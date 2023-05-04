using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAppNotifications;

public class DeleteAppNotificationsRequest : BaseRequest
{
    public List<Guid> AppNotificationIds { get; set; } = new();
}