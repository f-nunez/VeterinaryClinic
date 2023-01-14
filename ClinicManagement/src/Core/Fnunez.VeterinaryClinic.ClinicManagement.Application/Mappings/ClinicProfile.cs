using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

public class ClinicProfile : Profile
{
    public ClinicProfile()
    {
        CreateMap<Clinic, ClinicDto>();
    }
}