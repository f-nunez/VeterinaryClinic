using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Mappings;

public class AppointmentTypeProfile : Profile
{
    public AppointmentTypeProfile()
    {
        CreateMap<AppointmentType, AppointmentTypeDto>();
    }
}