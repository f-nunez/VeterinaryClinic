using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.CreatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.DeletePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.UpdatePatient;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<Patient, PatientDto>()
            .ForMember(
                dto => dto.PatientId,
                options => options.MapFrom(src => src.Id)
            ).ForMember(
                dto => dto.ClientName,
                options => options.MapFrom(src => string.Empty)
            );

        CreateMap<PatientDto, Patient>()
            .ForMember(
                dto => dto.Id,
                options => options.MapFrom(src => src.PatientId)
            );

        CreateMap<CreatePatientRequest, Patient>();

        CreateMap<UpdatePatientRequest, Patient>()
            .ForMember(
                dto => dto.Id,
                options => options.MapFrom(src => src.PatientId)
            );

        CreateMap<DeletePatientRequest, Patient>();
    }
}