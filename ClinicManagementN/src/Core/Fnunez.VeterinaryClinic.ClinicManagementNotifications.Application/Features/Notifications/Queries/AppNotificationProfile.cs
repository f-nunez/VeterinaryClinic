using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Application.Features.Notifications.Queries;

public class AppNotificationProfile : Profile
{
    public AppNotificationProfile()
    {
        CreateMap<AppNotification, AppNotificationDto>()
            .ForMember(
                dto => dto.CreatedOn,
                options => options.MapFrom(src => src.CreatedOn)
            ).ForMember(
                dto => dto.Event,
                options => options.MapFrom(src => src.Notification.NotificationEvent.ToString())
            ).ForMember(
                dto => dto.Id,
                options => options.MapFrom(src => src.Id)
            ).ForMember(
                dto => dto.IsRead,
                options => options.MapFrom(src => src.ReadOn.HasValue)
            ).ForMember(
                dto => dto.Payload,
                options => options.MapFrom(src => src.Notification.Payload)
            ).ForMember(
                dto => dto.TriggeredBy,
                options => options.MapFrom(src => src.Notification.TriggeredByUser.Name)
            );
    }
}