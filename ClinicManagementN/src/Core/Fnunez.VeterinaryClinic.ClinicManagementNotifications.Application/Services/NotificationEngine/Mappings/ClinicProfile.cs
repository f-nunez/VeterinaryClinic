using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;

public class ClinicProfile : Profile
{
    public ClinicProfile()
    {
        CreateMap<ClinicCreatedNotificationRequest, ClinicCreatedPayload>()
            .ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            );

        CreateMap<ClinicDeletedNotificationRequest, ClinicDeletedPayload>()
            .ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            );

        CreateMap<ClinicUpdatedNotificationRequest, ClinicUpdatedPayload>()
            .ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            );
    }
}