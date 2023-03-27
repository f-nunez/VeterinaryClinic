using AutoMapper;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Mappings;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<AppointmentConfirmedEmailRequest, AppointmentConfirmedPayload>();
        CreateMap<AppointmentCreatedEmailRequest, AppointmentCreatedPayload>();
        CreateMap<AppointmentDeletedEmailRequest, AppointmentDeletedPayload>();
        CreateMap<AppointmentUpdatedEmailRequest, AppointmentUpdatedPayload>();
    }
}