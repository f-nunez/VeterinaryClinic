using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

public class AppointmentTypeProfile : Profile
{
    public AppointmentTypeProfile()
    {
        CreateMap<AppointmentType, AppointmentTypeDto>();

        CreateMap<AppointmentTypeDto, AppointmentType>();

        CreateMap<CreateAppointmentTypeRequest, AppointmentType>();
    }
}