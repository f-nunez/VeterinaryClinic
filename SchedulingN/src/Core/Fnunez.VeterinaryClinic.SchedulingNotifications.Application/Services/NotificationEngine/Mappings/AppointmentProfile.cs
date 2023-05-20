using AutoMapper;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Mappings;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<AppointmentConfirmedNotificationRequest, AppointmentConfirmedPayload>()
            .ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.Title,
                m => m.MapFrom(s => s.Title)
            );

        CreateMap<AppointmentCreatedNotificationRequest, AppointmentCreatedPayload>()
            .ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.Title,
                m => m.MapFrom(s => s.Title)
            );

        CreateMap<AppointmentDeletedNotificationRequest, AppointmentDeletedPayload>()
            .ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.Title,
                m => m.MapFrom(s => s.Title)
            );

        CreateMap<AppointmentUpdatedNotificationRequest, AppointmentUpdatedPayload>()
            .ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.Title,
                m => m.MapFrom(s => s.Title)
            );
    }
}