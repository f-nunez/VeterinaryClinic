using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Doctors;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;

public static class DoctorHelper
{
    public static DoctorVm MapDoctorViewModel(DoctorDto doctorDto)
    {
        return new DoctorVm
        {
            FullName = doctorDto.FullName,
            Id = doctorDto.Id
        };
    }
}