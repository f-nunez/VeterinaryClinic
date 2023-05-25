using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.CreateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

public class ClinicProfile : Profile
{
    public ClinicProfile()
    {
        CreateMap<Clinic, ClinicDto>()
            .ForMember(
                d => d.Address,
                m => m.MapFrom(s => s.Address)
            ).ForMember(
                d => d.EmailAddress,
                m => m.MapFrom(s => s.EmailAddress)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.IsActive,
                m => m.MapFrom(s => s.IsActive)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            );

        CreateMap<ClinicDto, Clinic>()
            .ForMember(
                d => d.Address,
                m => m.MapFrom(s => s.Address)
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
                d => d.EmailAddress,
                m => m.MapFrom(s => s.EmailAddress)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.IsActive,
                m => m.MapFrom(s => s.IsActive)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            ).ForMember(
                d => d.UpdatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.UpdatedOn,
                m => m.Ignore()
            );

        CreateMap<CreateClinicRequest, Clinic>()
            .ForMember(
                d => d.Address,
                m => m.MapFrom(s => s.Address)
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
                d => d.EmailAddress,
                m => m.MapFrom(s => s.EmailAddress)
            ).ForMember(
                d => d.Id,
                m => m.Ignore()
            ).ForMember(
                d => d.IsActive,
                m => m.Ignore()
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            ).ForMember(
                d => d.UpdatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.UpdatedOn,
                m => m.Ignore()
            );
    }
}