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
                d => d.ClientId,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.EmailAddress,
                m => m.MapFrom(s => s.EmailAddress)
            ).ForMember(
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.PreferredDoctorId,
                m => m.MapFrom(s => s.PreferredDoctorId)
            ).ForMember(
                d => d.PreferredLanguage,
                m => m.MapFrom(s => s.PreferredLanguage)
            ).ForMember(
                d => d.Salutation,
                m => m.MapFrom(s => s.Salutation)
            );

        CreateMap<Client, ClientDetailDto>()
            .ForMember(
                d => d.ClientId,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.EmailAddress,
                m => m.MapFrom(s => s.EmailAddress)
            ).ForMember(
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.PreferredDoctorFullName,
                m => m.MapFrom(s => s.PreferredDoctor.FullName)
            ).ForMember(
                d => d.PreferredLanguage,
                m => m.MapFrom(s => s.PreferredLanguage)
            ).ForMember(
                d => d.PreferredName,
                m => m.MapFrom(s => s.PreferredName)
            ).ForMember(
                d => d.Salutation,
                m => m.MapFrom(s => s.Salutation)
            );
    }
}