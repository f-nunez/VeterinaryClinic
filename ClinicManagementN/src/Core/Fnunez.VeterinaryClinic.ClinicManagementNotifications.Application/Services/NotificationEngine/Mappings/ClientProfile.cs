using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientCreatedNotificationRequest, ClientCreatedPayload>()
            .ForMember(
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            );

        CreateMap<ClientDeletedNotificationRequest, ClientDeletedPayload>()
            .ForMember(
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            );

        CreateMap<ClientUpdatedNotificationRequest, ClientUpdatedPayload>()
            .ForMember(
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            );
    }
}