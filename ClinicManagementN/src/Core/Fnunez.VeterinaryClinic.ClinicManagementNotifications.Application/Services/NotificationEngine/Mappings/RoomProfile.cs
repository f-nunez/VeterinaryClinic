using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<RoomCreatedNotificationRequest, RoomCreatedPayload>();
        CreateMap<RoomDeletedNotificationRequest, RoomDeletedPayload>();
        CreateMap<RoomUpdatedNotificationRequest, RoomUpdatedPayload>();
    }
}