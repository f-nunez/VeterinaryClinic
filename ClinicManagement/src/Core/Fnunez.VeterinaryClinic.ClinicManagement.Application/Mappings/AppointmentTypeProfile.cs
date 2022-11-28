using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

public class AppointmentTypeProfile : Profile
{
    public AppointmentTypeProfile()
    {
        CreateMap<AppointmentType, AppointmentTypeDto>()
            .ForMember(
                dto => dto.AppointmentTypeId,
                options => options.MapFrom(src => src.Id)
            );

        CreateMap<AppointmentTypeDto, AppointmentType>()
            .ForMember(
                dto => dto.Id,
                options => options.MapFrom(src => src.AppointmentTypeId)
            );
    }
}