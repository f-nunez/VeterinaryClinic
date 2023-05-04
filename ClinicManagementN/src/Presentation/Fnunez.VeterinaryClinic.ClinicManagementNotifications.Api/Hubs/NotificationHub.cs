using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Api.Hubs;

[Authorize]
public class NotificationHub : Hub
{
}