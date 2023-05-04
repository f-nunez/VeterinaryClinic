using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.Clinics;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;

public static class ClinicHelper
{
    public static ClinicVm MapClinicViewModel(ClinicDto clinicDto)
    {
        return new ClinicVm
        {
            Address = clinicDto.Address,
            EmailAddress = clinicDto.EmailAddress,
            Id = clinicDto.Id,
            Name = clinicDto.Name
        };
    }
}