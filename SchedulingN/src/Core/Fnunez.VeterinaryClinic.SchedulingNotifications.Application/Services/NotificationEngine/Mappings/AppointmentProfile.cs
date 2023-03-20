using AutoMapper;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Mappings;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<AppointmentCreatedNotificationRequest, AppointmentCreatedPayload>();
        CreateMap<AppointmentDeletedNotificationRequest, AppointmentDeletedPayload>();
        CreateMap<AppointmentUpdatedNotificationRequest, AppointmentUpdatedPayload>();
    }
}