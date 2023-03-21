using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Common.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAppNotification;

public class DeleteAppNotificationRequest : BaseRequest
{
    public Guid AppNotificationId { get; set; }
}