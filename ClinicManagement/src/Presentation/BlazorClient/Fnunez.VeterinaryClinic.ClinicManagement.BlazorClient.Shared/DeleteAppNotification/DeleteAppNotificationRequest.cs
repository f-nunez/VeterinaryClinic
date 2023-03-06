using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.DeleteAppNotification;

public class DeleteAppNotificationRequest : BaseRequest
{
    public Guid AppNotificationId { get; set; }
}