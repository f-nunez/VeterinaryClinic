using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<Doctor, DoctorDto>()
            .ForMember(
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.IsActive,
                m => m.MapFrom(s => s.IsActive)
            );

        CreateMap<DoctorDto, Doctor>()
            .ForMember(
                d => d.CreatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.CreatedOn,
                m => m.Ignore()
            ).ForMember(
                d => d.DomainEvents,
                m => m.Ignore()
            ).ForMember(
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.IsActive,
                m => m.MapFrom(s => s.IsActive)
            ).ForMember(
                d => d.UpdatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.UpdatedOn,
                m => m.Ignore()
            );

        CreateMap<CreateDoctorRequest, Doctor>()
            .ForMember(
                d => d.CreatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.CreatedOn,
                m => m.Ignore()
            ).ForMember(
                d => d.DomainEvents,
                m => m.Ignore()
            ).ForMember(
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.Id,
                m => m.Ignore()
            ).ForMember(
                d => d.IsActive,
                m => m.Ignore()
            ).ForMember(
                d => d.UpdatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.UpdatedOn,
                m => m.Ignore()
            );
    }
}