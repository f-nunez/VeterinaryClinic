using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAllAppNotifications;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.DeleteAppNotification;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Commands.MarkAppNotificationAsRead;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetAppNotifications;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetAppNotificationsDataGrid;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries.GetUnreadAppNotificationsCount;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Api.Controllers;

public class NotificationController : BaseApiController
{
    [HttpDelete("DeleteAllAppNotifications")]
    public async Task<ActionResult> DeleteAllAppNotifications(
        [FromRoute] DeleteAllAppNotificationsRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteAllAppNotificationsCommand(request);

        DeleteAllAppNotificationsResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("DeleteAppNotification/{AppNotificationId}")]
    public async Task<ActionResult> DeleteAppNotification(
        [FromRoute] DeleteAppNotificationRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteAppNotificationCommand(request);

        DeleteAppNotificationResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPost("GetAppNotifications")]
    public async Task<ActionResult> GetAppNotifications(
        GetAppNotificationsRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppNotificationsQuery(request);

        GetAppNotificationsResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("GetAppNotificationsDataGrid")]
    public async Task<ActionResult> GetAppNotificationsDataGrid(
        GetAppNotificationsDataGridRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetAppNotificationsDataGridQuery(request);

        GetAppNotificationsDataGridResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost("GetUnreadAppNotificationsCount")]
    public async Task<ActionResult> GetUnreadAppNotificationsCount(
        GetUnreadAppNotificationsCountRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetUnreadAppNotificationsCountQuery(request);

        GetUnreadAppNotificationsCountResponse response = await Mediator
            .Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPut("MarkAppNotificationAsRead")]
    public async Task<ActionResult> MarkAppNotificationAsRead(
        MarkAppNotificationAsReadRequest request,
        CancellationToken cancellationToken)
    {
        var command = new MarkAppNotificationAsReadCommand(request);

        MarkAppNotificationAsReadResponse response = await Mediator
            .Send(command, cancellationToken);

        return Ok(response);
    }
}