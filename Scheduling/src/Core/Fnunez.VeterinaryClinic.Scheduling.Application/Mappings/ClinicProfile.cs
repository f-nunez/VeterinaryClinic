using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Mappings;

public class ClinicProfile : Profile
{
    public ClinicProfile()
    {
        CreateMap<Clinic, ClinicDto>();
    }
}