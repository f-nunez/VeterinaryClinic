using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<PatientCreatedNotificationRequest, PatientCreatedPayload>()
            .ForMember(
                d => d.ClientId,
                m => m.MapFrom(s => s.ClientId)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            ).ForMember(
                d => d.PatientId,
                m => m.MapFrom(s => s.PatientId)
            );

        CreateMap<PatientDeletedNotificationRequest, PatientDeletedPayload>()
            .ForMember(
                d => d.ClientId,
                m => m.MapFrom(s => s.ClientId)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            ).ForMember(
                d => d.PatientId,
                m => m.MapFrom(s => s.PatientId)
            );

        CreateMap<PatientUpdatedNotificationRequest, PatientUpdatedPayload>()
            .ForMember(
                d => d.ClientId,
                m => m.MapFrom(s => s.ClientId)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            ).ForMember(
                d => d.PatientId,
                m => m.MapFrom(s => s.PatientId)
            );
    }
}