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
        CreateMap<Doctor, DoctorDto>();

        CreateMap<DoctorDto, Doctor>()
            .ConstructUsing(
                dto => new Doctor(dto.Id, dto.FullName)
            );

        CreateMap<CreateDoctorRequest, Doctor>();

        CreateMap<UpdateDoctorRequest, Doctor>();

        CreateMap<DeleteDoctorRequest, Doctor>();
    }
}