using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Mappings;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<Client, ClientDto>()
            .ForMember(
                dto => dto.ClientId,
                options => options.MapFrom(src => src.Id)
            );
    }
}