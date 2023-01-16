using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.CreateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.DeleteClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.UpdateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

public class ClinicProfile : Profile
{
    public ClinicProfile()
    {
        CreateMap<Clinic, ClinicDto>();

        CreateMap<ClinicDto, Clinic>();

        CreateMap<CreateClinicRequest, Clinic>();

        CreateMap<DeleteClinicRequest, Clinic>();

        CreateMap<UpdateClinicRequest, Clinic>();
    }
}