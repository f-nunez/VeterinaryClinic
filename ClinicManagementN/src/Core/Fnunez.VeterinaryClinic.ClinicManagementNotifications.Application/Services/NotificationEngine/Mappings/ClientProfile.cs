using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Payloads;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Requests;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Services.NotificationEngine.Mappings;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientCreatedNotificationRequest, ClientCreatedPayload>();
        CreateMap<ClientDeletedNotificationRequest, ClientDeletedPayload>();
        CreateMap<ClientUpdatedNotificationRequest, ClientUpdatedPayload>();
    }
}