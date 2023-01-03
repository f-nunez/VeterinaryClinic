using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterPatient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Patient;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Mappings;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<Patient, PatientDto>()
            .ForMember(
                dto => dto.PatientId,
                options => options.MapFrom(src => src.Id)
            ).ForMember(
                dto => dto.PatientName,
                options => options.MapFrom(src => src.Name)
            ).ForMember(
                dto => dto.ClientName,
                options => options.MapFrom(src => string.Empty)
            );
        
        CreateMap<Patient, PatientFilterValueDto>();
    }
}