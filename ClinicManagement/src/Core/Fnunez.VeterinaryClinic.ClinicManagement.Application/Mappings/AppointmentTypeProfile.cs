using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

public class AppointmentTypeProfile : Profile
{
    public AppointmentTypeProfile()
    {
        CreateMap<AppointmentType, AppointmentTypeDto>()
            .ForMember(
                d => d.Code,
                m => m.MapFrom(s => s.Code)
            ).ForMember(
                d => d.Duration,
                m => m.MapFrom(s => s.Duration)
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

        CreateMap<AppointmentTypeDto, AppointmentType>()
            .ForMember(
                d => d.Code,
                m => m.MapFrom(s => s.Code)
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
                d => d.Duration,
                m => m.MapFrom(s => s.Duration)
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

        CreateMap<CreateAppointmentTypeRequest, AppointmentType>()
            .ForMember(
                d => d.Code,
                m => m.MapFrom(s => s.Code)
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
                d => d.Duration,
                m => m.MapFrom(s => s.Duration)
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