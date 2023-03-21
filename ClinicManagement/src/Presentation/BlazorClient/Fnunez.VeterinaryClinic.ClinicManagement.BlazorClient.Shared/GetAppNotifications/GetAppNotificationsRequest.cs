using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Shared.GetAppNotifications;

public class GetAppNotificationsRequest : BaseRequest
{
    public int Skip { get; set; }
    public int Take { get; set; }
}