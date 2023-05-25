using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterPatient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Mappings;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<Patient, PatientDto>()
            .ForMember(
                d => d.ClientId,
                m => m.MapFrom(s => s.ClientId)
            ).ForMember(
                d => d.ClientName,
                m => m.Ignore()
            ).ForMember(
                d => d.ImageData,
                m => m.Ignore()
            ).ForMember(
                d => d.PatientId,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.PatientName,
                m => m.MapFrom(s => s.Name)
            ).ForMember(
                d => d.PreferredDoctorId,
                m => m.MapFrom(s => s.PreferredDoctorId)
            );

        CreateMap<Patient, PatientFilterValueDto>()
            .ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            );
    }
}