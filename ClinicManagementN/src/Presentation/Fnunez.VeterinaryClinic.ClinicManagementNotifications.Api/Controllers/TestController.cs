using System.Text.Json;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.NotificationAggregate.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Api.Controllers;

public class TestController : BaseApiController
{
    private readonly INotificationEngineService _notificationEngineService;

    public TestController(INotificationEngineService notificationEngineService)
    {
        _notificationEngineService = notificationEngineService;
    }

    [HttpGet("CreateNotification")]
    public async Task<string> CreateNotification()
    {
        var notificationRequest = new AppointmentTypeCreatedNotificationRequest
        {
            CorrelationId = Guid.NewGuid(),
            Id = 1,
            Name = "Test",
            TriggeredByUserId = "9f79b45e-1ebe-4bb2-9d6f-e00da51b0848"
        };

        string serializedNotificationRequest = JsonSerializer.Serialize(notificationRequest);

        var appNotifications = await _notificationEngineService
            .CreateAndNotifyAsync(NotificationEvent.AppointmentTypeCreated.ToString(), serializedNotificationRequest);

        return JsonSerializer.Serialize(appNotifications);
    }
}