using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.MarkAppNotificationAsRead;

public class MarkAppNotificationAsReadRequest : BaseRequest
{
    public Guid AppNotificationId { get; set; }
}