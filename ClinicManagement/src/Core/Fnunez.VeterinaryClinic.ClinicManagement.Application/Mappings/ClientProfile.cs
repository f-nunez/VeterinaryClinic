using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.UpdateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<Client, ClientDto>()
            .ForMember(
                dto => dto.ClientId,
                options => options.MapFrom(src => src.Id)
            );

        CreateMap<ClientDto, Client>()
            .ForMember(
                dto => dto.Id,
                options => options.MapFrom(src => src.ClientId)
            );

        CreateMap<CreateClientRequest, Client>()
            .ForMember(
                dto => dto.Patients,
                options => options.Ignore()
            );

        CreateMap<UpdateClientRequest, Client>()
            .ForMember(
                dto => dto.Id,
                options => options.MapFrom(src => src.ClientId)
            );

        CreateMap<DeleteClientRequest, Client>();

        CreateMap<Patient, int>().ConvertUsing(src => src.Id);
    }
}