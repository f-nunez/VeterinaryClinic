using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.CreateClient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

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
                d => d.IsActive,
                m => m.MapFrom(s => s.IsActive)
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
                d => d.IsActive,
                m => m.MapFrom(s => s.IsActive)
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

        CreateMap<ClientDto, Client>()
            .ForMember(
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
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.ClientId)
            ).ForMember(
                d => d.IsActive,
                m => m.MapFrom(s => s.IsActive)
            ).ForMember(
                d => d.Patients,
                m => m.Ignore()
            ).ForMember(
                d => d.PreferredDoctor,
                m => m.Ignore()
            ).ForMember(
                d => d.PreferredDoctorId,
                m => m.MapFrom(s => s.PreferredDoctorId)
            ).ForMember(
                d => d.PreferredLanguage,
                m => m.MapFrom(s => s.PreferredLanguage)
            ).ForMember(
                d => d.PreferredName,
                m => m.MapFrom(s => s.PreferredName)
            ).ForMember(
                d => d.Salutation,
                m => m.MapFrom(s => s.Salutation)
            ).ForMember(
                d => d.UpdatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.UpdatedOn,
                m => m.Ignore()
            );

        CreateMap<CreateClientRequest, Client>()
            .ForMember(
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
                d => d.FullName,
                m => m.MapFrom(s => s.FullName)
            ).ForMember(
                d => d.Id,
                m => m.Ignore()
            ).ForMember(
                d => d.IsActive,
                m => m.Ignore()
            ).ForMember(
                d => d.Patients,
                m => m.Ignore()
            ).ForMember(
                d => d.PreferredDoctor,
                m => m.Ignore()
            ).ForMember(
                d => d.PreferredDoctorId,
                m => m.MapFrom(s => s.PreferredDoctorId)
            ).ForMember(
                d => d.PreferredLanguage,
                m => m.MapFrom(s => s.PreferredLanguage)
            ).ForMember(
                d => d.PreferredName,
                m => m.MapFrom(s => s.PreferredName)
            ).ForMember(
                d => d.Salutation,
                m => m.MapFrom(s => s.Salutation)
            ).ForMember(
                d => d.UpdatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.UpdatedOn,
                m => m.Ignore()
            );

        CreateMap<Patient, int>().ConvertUsing(s => s.Id);
    }
}