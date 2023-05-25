using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;

public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<DoctorCreatedNotificationRequest, DoctorCreatedPayload>()
            .ForMember(
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            );

        CreateMap<DoctorDeletedNotificationRequest, DoctorDeletedPayload>()
            .ForMember(
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            );

        CreateMap<DoctorUpdatedNotificationRequest, DoctorUpdatedPayload>()
            .ForMember(
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            );
    }
}