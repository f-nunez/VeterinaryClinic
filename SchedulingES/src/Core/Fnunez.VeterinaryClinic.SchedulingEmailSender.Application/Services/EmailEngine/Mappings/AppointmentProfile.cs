using AutoMapper;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Payloads;
using Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Requests;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.EmailEngine.Mappings;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<AppointmentConfirmedEmailRequest, AppointmentConfirmedPayload>()
            .ForMember(
                d => d.AppointmentEndOn,
                m => m.MapFrom(s => s.AppointmentEndOn)
            ).ForMember(
                d => d.AppointmentId,
                m => m.MapFrom(s => s.AppointmentId)
            ).ForMember(
                d => d.AppointmentStartOn,
                m => m.MapFrom(s => s.AppointmentStartOn)
            ).ForMember(
                d => d.ClientFullName,
                m => m.MapFrom(s => s.ClientFullName)
            ).ForMember(
                d => d.ClinicAddress,
                m => m.MapFrom(s => s.ClinicAddress)
            ).ForMember(
                d => d.ClinicName,
                m => m.MapFrom(s => s.ClinicName)
            ).ForMember(
                d => d.DoctorFullName,
                m => m.MapFrom(s => s.DoctorFullName)
            ).ForMember(
                d => d.Language,
                m => m.MapFrom(s => s.Language)
            ).ForMember(
                d => d.PatientName,
                m => m.MapFrom(s => s.PatientName)
            ).ForMember(
                d => d.SendTo,
                m => m.MapFrom(s => s.SendTo)
            );

        CreateMap<AppointmentCreatedEmailRequest, AppointmentCreatedPayload>()
            .ForMember(
                d => d.AppointmentEndOn,
                m => m.MapFrom(s => s.AppointmentEndOn)
            ).ForMember(
                d => d.AppointmentId,
                m => m.MapFrom(s => s.AppointmentId)
            ).ForMember(
                d => d.AppointmentStartOn,
                m => m.MapFrom(s => s.AppointmentStartOn)
            ).ForMember(
                d => d.ClientFullName,
                m => m.MapFrom(s => s.ClientFullName)
            ).ForMember(
                d => d.ClinicAddress,
                m => m.MapFrom(s => s.ClinicAddress)
            ).ForMember(
                d => d.ClinicName,
                m => m.MapFrom(s => s.ClinicName)
            ).ForMember(
                d => d.DoctorFullName,
                m => m.MapFrom(s => s.DoctorFullName)
            ).ForMember(
                d => d.Language,
                m => m.MapFrom(s => s.Language)
            ).ForMember(
                d => d.PatientName,
                m => m.MapFrom(s => s.PatientName)
            ).ForMember(
                d => d.SendTo,
                m => m.MapFrom(s => s.SendTo)
            );

        CreateMap<AppointmentDeletedEmailRequest, AppointmentDeletedPayload>()
            .ForMember(
                d => d.AppointmentEndOn,
                m => m.MapFrom(s => s.AppointmentEndOn)
            ).ForMember(
                d => d.AppointmentId,
                m => m.MapFrom(s => s.AppointmentId)
            ).ForMember(
                d => d.AppointmentStartOn,
                m => m.MapFrom(s => s.AppointmentStartOn)
            ).ForMember(
                d => d.ClientFullName,
                m => m.MapFrom(s => s.ClientFullName)
            ).ForMember(
                d => d.ClinicAddress,
                m => m.MapFrom(s => s.ClinicAddress)
            ).ForMember(
                d => d.ClinicName,
                m => m.MapFrom(s => s.ClinicName)
            ).ForMember(
                d => d.DoctorFullName,
                m => m.MapFrom(s => s.DoctorFullName)
            ).ForMember(
                d => d.Language,
                m => m.MapFrom(s => s.Language)
            ).ForMember(
                d => d.PatientName,
                m => m.MapFrom(s => s.PatientName)
            ).ForMember(
                d => d.SendTo,
                m => m.MapFrom(s => s.SendTo)
            );

        CreateMap<AppointmentUpdatedEmailRequest, AppointmentUpdatedPayload>()
            .ForMember(
                d => d.AppointmentEndOn,
                m => m.MapFrom(s => s.AppointmentEndOn)
            ).ForMember(
                d => d.AppointmentId,
                m => m.MapFrom(s => s.AppointmentId)
            ).ForMember(
                d => d.AppointmentStartOn,
                m => m.MapFrom(s => s.AppointmentStartOn)
            ).ForMember(
                d => d.ClientFullName,
                m => m.MapFrom(s => s.ClientFullName)
            ).ForMember(
                d => d.ClinicAddress,
                m => m.MapFrom(s => s.ClinicAddress)
            ).ForMember(
                d => d.ClinicName,
                m => m.MapFrom(s => s.ClinicName)
            ).ForMember(
                d => d.DoctorFullName,
                m => m.MapFrom(s => s.DoctorFullName)
            ).ForMember(
                d => d.Language,
                m => m.MapFrom(s => s.Language)
            ).ForMember(
                d => d.PatientName,
                m => m.MapFrom(s => s.PatientName)
            ).ForMember(
                d => d.SendTo,
                m => m.MapFrom(s => s.SendTo)
            );
    }
}