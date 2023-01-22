using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Clinics;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;

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