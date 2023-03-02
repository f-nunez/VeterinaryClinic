using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;

public class ClinicProfile : Profile
{
    public ClinicProfile()
    {
        CreateMap<ClinicCreatedNotificationRequest, ClinicCreatedPayload>();
        CreateMap<ClinicDeletedNotificationRequest, ClinicDeletedPayload>();
        CreateMap<ClinicUpdatedNotificationRequest, ClinicUpdatedPayload>();
    }
}