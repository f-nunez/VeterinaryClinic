using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientDetail;
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
        
        CreateMap<Client, ClientDetailDto>()
            .ForMember(
                dto => dto.ClientId,
                options => options.MapFrom(src => src.Id)
            ).ForMember(
                dto => dto.PreferredDoctorFullName,
                options => options.MapFrom(src => src.PreferredDoctor.FullName)
            );
    }
}