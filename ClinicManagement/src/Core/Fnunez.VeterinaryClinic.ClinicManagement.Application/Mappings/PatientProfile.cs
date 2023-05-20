using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

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
                d => d.IsActive,
                m => m.MapFrom(s => s.IsActive)
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

        CreateMap<PatientDto, Patient>()
            .ForMember(
                d => d.AnimalSex,
                m => m.Ignore()
            ).ForMember(
                d => d.AnimalType,
                m => m.Ignore()
            ).ForMember(
                d => d.ClientId,
                m => m.MapFrom(s => s.ClientId)
            ).ForMember(
                d => d.CreatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.CreatedOn,
                m => m.Ignore()
            ).ForMember(
                d => d.DomainEvents,
                m => m.Ignore()
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.PatientId)
            ).ForMember(
                d => d.IsActive,
                m => m.MapFrom(s => s.IsActive)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.PatientName)
            ).ForMember(
                d => d.Photo,
                m => m.Ignore()
            ).ForMember(
                d => d.PreferredDoctorId,
                m => m.MapFrom(s => s.PreferredDoctorId)
            ).ForMember(
                d => d.UpdatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.UpdatedOn,
                m => m.Ignore()
            );
    }
}