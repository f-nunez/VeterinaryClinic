using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<RoomCreatedNotificationRequest, RoomCreatedPayload>()
            .ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            );

        CreateMap<RoomDeletedNotificationRequest, RoomDeletedPayload>()
            .ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            );

        CreateMap<RoomUpdatedNotificationRequest, RoomUpdatedPayload>()
            .ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            );
    }
}