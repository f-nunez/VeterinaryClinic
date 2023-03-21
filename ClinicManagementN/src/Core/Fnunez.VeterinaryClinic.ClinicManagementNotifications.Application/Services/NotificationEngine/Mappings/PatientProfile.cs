using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<PatientCreatedNotificationRequest, PatientCreatedPayload>();
        CreateMap<PatientDeletedNotificationRequest, PatientDeletedPayload>();
        CreateMap<PatientUpdatedNotificationRequest, PatientUpdatedPayload>();
    }
}