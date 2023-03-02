using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;

public class AppointmentTypeProfile : Profile
{
    public AppointmentTypeProfile()
    {
        CreateMap<AppointmentTypeCreatedNotificationRequest, AppointmentTypeCreatedPayload>();
        CreateMap<AppointmentTypeDeletedNotificationRequest, AppointmentTypeDeletedPayload>();
        CreateMap<AppointmentTypeUpdatedNotificationRequest, AppointmentTypeUpdatedPayload>();
    }
}