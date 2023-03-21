using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;

public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<DoctorCreatedNotificationRequest, DoctorCreatedPayload>();
        CreateMap<DoctorDeletedNotificationRequest, DoctorDeletedPayload>();
        CreateMap<DoctorUpdatedNotificationRequest, DoctorUpdatedPayload>();
    }
}