using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;

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
                dto => dto.ClientFullName,
                options => options.MapFrom(src => src.Client.FullName)
            ).ForMember(
                dto => dto.ClientId,
                options => options.MapFrom(src => src.Client.Id)
            ).ForMember(
                dto => dto.ClinicId,
                options => options.MapFrom(src => src.Clinic.Id)
            ).ForMember(
                dto => dto.ClinicName,
                options => options.MapFrom(src => src.Clinic.Name)
            ).ForMember(
                dto => dto.DoctorFullName,
                options => options.MapFrom(src => src.Doctor.FullName)
            ).ForMember(
                dto => dto.DoctorId,
                options => options.MapFrom(src => src.Doctor.Id)
            ).ForMember(
                dto => dto.IsConfirmed,
                options => options.MapFrom(src => src.ConfirmOn.HasValue)
            ).ForMember(
                dto => dto.PatientId,
                options => options.MapFrom(src => src.Patient.Id)
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
        
        CreateMap<Appointment, AppointmentDetailDto>()
            .ForMember(
                dto => dto.AppointmentId,
                option => option.MapFrom(src => src.Id)
            ).ForMember(
                dto => dto.AppointmentTypeName,
                option => option.MapFrom(src => src.AppointmentType.Name)
            ).ForMember(
                dto => dto.ClientFullName,
                option => option.MapFrom(src => src.Client.FullName)
            ).ForMember(
                dto => dto.ClinicName,
                option => option.MapFrom(src => src.Clinic.Name)
            ).ForMember(
                dto => dto.DoctorFullName,
                option => option.MapFrom(src => src.Doctor.FullName)
            ).ForMember(
                dto => dto.IsConfirmed,
                option => option.MapFrom(src => src.ConfirmOn.HasValue)
            ).ForMember(
                dto => dto.PatientName,
                option => option.MapFrom(src => src.Patient.Name)
            ).ForMember(
                dto => dto.RoomName,
                option => option.MapFrom(src => src.Room.Name)
            ).ForMember(
                dto => dto.StartOn,
                option => option.MapFrom(src => src.DateRange.StartOn)
            ).ForMember(
                dto => dto.EndOn,
                option => option.MapFrom(src => src.DateRange.EndOn)
            );
    }
}