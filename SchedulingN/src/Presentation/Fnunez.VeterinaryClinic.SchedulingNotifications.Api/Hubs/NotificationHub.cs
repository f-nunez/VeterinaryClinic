using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Api.Hubs;

[Authorize]
public class NotificationHub : Hub
{
}