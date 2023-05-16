using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentEdit;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Mappings;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(
                d => d.AppointmentId,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.AppointmentTypeId,
                m => m.MapFrom(s => s.AppointmentTypeId)
            ).ForMember(
                d => d.ClientFullName,
                m => m.MapFrom(s => s.Client.FullName)
            ).ForMember(
                d => d.ClientId,
                m => m.MapFrom(s => s.Client.Id)
            ).ForMember(
                d => d.ClinicId,
                m => m.MapFrom(s => s.Clinic.Id)
            ).ForMember(
                d => d.ClinicName,
                m => m.MapFrom(s => s.Clinic.Name)
            ).ForMember(
                d => d.Description,
                m => m.MapFrom(s => s.Description)
            ).ForMember(
                d => d.DoctorFullName,
                m => m.MapFrom(s => s.Doctor.FullName)
            ).ForMember(
                d => d.DoctorId,
                m => m.MapFrom(s => s.Doctor.Id)
            ).ForMember(
                d => d.IsConfirmed,
                m => m.MapFrom(s => s.ConfirmOn.HasValue)
            ).ForMember(
                d => d.PatientId,
                m => m.MapFrom(s => s.Patient.Id)
            ).ForMember(
                d => d.PatientName,
                m => m.MapFrom(s => s.Patient.Name)
            ).ForMember(
                d => d.RoomId,
                m => m.MapFrom(s => s.RoomId)
            ).ForMember(
                d => d.RoomName,
                m => m.MapFrom(s => s.Room.Name)
            ).ForMember(
                d => d.StartOn,
                m => m.MapFrom(s => s.DateRange.StartOn)
            ).ForMember(
                d => d.EndOn,
                m => m.MapFrom(s => s.DateRange.EndOn)
            ).ForMember(
                d => d.Title,
                m => m.MapFrom(s => s.Title)
            );

        CreateMap<Appointment, AppointmentDetailDto>()
            .ForMember(
                d => d.AppointmentId,
                option => option.MapFrom(s => s.Id)
            ).ForMember(
                d => d.AppointmentTypeName,
                option => option.MapFrom(s => s.AppointmentType.Name)
            ).ForMember(
                d => d.ClientFullName,
                option => option.MapFrom(s => s.Client.FullName)
            ).ForMember(
                d => d.ClinicName,
                option => option.MapFrom(s => s.Clinic.Name)
            ).ForMember(
                d => d.Description,
                option => option.MapFrom(s => s.Description)
            ).ForMember(
                d => d.DoctorFullName,
                option => option.MapFrom(s => s.Doctor.FullName)
            ).ForMember(
                d => d.IsActive,
                option => option.MapFrom(s => s.IsActive)
            ).ForMember(
                d => d.IsConfirmed,
                option => option.MapFrom(s => s.ConfirmOn.HasValue)
            ).ForMember(
                d => d.PatientName,
                option => option.MapFrom(s => s.Patient.Name)
            ).ForMember(
                d => d.PatientPhotoData,
                option => option.Ignore()
            ).ForMember(
                d => d.RoomName,
                option => option.MapFrom(s => s.Room.Name)
            ).ForMember(
                d => d.StartOn,
                option => option.MapFrom(s => s.DateRange.StartOn)
            ).ForMember(
                d => d.EndOn,
                option => option.MapFrom(s => s.DateRange.EndOn)
            ).ForMember(
                d => d.Title,
                option => option.MapFrom(s => s.Title)
            );

        CreateMap<Appointment, AppointmentEditDto>()
            .ForMember(
                d => d.AppointmentId,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.AppointmentTypeDuration,
                m => m.MapFrom(s => s.AppointmentType.Duration)
            ).ForMember(
                d => d.AppointmentTypeId,
                m => m.MapFrom(s => s.AppointmentTypeId)
            ).ForMember(
                d => d.ClientFullName,
                m => m.MapFrom(s => s.Client.FullName)
            ).ForMember(
                d => d.ClientId,
                m => m.MapFrom(s => s.Client.Id)
            ).ForMember(
                d => d.ClinicId,
                m => m.MapFrom(s => s.Clinic.Id)
            ).ForMember(
                d => d.ClinicName,
                m => m.MapFrom(s => s.Clinic.Name)
            ).ForMember(
                d => d.Description,
                m => m.MapFrom(s => s.Description)
            ).ForMember(
                d => d.DoctorFullName,
                m => m.MapFrom(s => s.Doctor.FullName)
            ).ForMember(
                d => d.DoctorId,
                m => m.MapFrom(s => s.Doctor.Id)
            ).ForMember(
                d => d.IsConfirmed,
                m => m.MapFrom(s => s.ConfirmOn.HasValue)
            ).ForMember(
                d => d.PatientId,
                m => m.MapFrom(s => s.Patient.Id)
            ).ForMember(
                d => d.PatientName,
                m => m.MapFrom(s => s.Patient.Name)
            ).ForMember(
                d => d.PatientPhotoData,
                m => m.Ignore()
            ).ForMember(
                d => d.RoomId,
                m => m.MapFrom(s => s.RoomId)
            ).ForMember(
                d => d.RoomName,
                m => m.MapFrom(s => s.Room.Name)
            ).ForMember(
                d => d.StartOn,
                m => m.MapFrom(s => s.DateRange.StartOn)
            ).ForMember(
                d => d.EndOn,
                m => m.MapFrom(s => s.DateRange.EndOn)
            ).ForMember(
                d => d.Title,
                m => m.MapFrom(s => s.Title)
            );
    }
}