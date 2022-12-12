using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.Entities;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Mappings;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(
                dto => dto.AppointmentId,
                options => options.MapFrom(src => src.Id)
            ).ForMember(
                dto => dto.ClientName,
                options => options.MapFrom(src => src.Client.FullName)
            ).ForMember(
                dto => dto.IsAllDay,
                options => options.MapFrom(src => false)
            ).ForMember(
                dto => dto.IsConfirmed,
                options => options.MapFrom(src => src.ConfirmOn.HasValue)
            ).ForMember(
                dto => dto.PatientName,
                options => options.MapFrom(src => src.Patient.Name)
            ).ForMember(
                dto => dto.StartOn,
                options => options.MapFrom(src => src.DateRange.StartOn)
            ).ForMember(
                dto => dto.EndOn,
                options => options.MapFrom(src => src.DateRange.EndOn)
            );
    }
}