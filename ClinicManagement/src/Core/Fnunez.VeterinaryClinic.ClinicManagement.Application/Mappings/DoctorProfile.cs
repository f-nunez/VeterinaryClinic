using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.DeleteDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<Doctor, DoctorDto>()
            .ForMember(
                dto => dto.DoctorId,
                options => options.MapFrom(src => src.Id)
            );

        CreateMap<DoctorDto, Doctor>()
            .ConstructUsing(
                dto => new Doctor(dto.DoctorId, dto.FullName)
            );

        CreateMap<CreateDoctorRequest, Doctor>()
            .ConstructUsing(dto => new Doctor(0, dto.FullName));

        CreateMap<UpdateDoctorRequest, Doctor>()
            .ForMember(
                dto => dto.Id,
                options => options.MapFrom(src => src.DoctorId)
            );

        CreateMap<DeleteDoctorRequest, Doctor>();
    }
}