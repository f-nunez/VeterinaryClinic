using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Mappings;

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
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            );
    }
}