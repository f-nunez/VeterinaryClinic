using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Doctors;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;

public static class DoctorHelper
{
    public static DoctorVm MapDoctorViewModel(DoctorDto doctorDto)
    {
        return new DoctorVm
        {
            FullName = doctorDto.FullName,
            Id = doctorDto.Id,
            IsActive = doctorDto.IsActive
        };
    }
}