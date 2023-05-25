using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Mappings;

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
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            );
    }
}