using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.AppointmentTypes;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;

public static class AppointmentTypeHelper
{
    public static AppointmentTypeVm MapAppointmentTypeViewModel(
        AppointmentTypeDto appointmentTypeDto)
    {
        return new AppointmentTypeVm
        {
            Code = appointmentTypeDto.Code,
            Duration = appointmentTypeDto.Duration,
            Id = appointmentTypeDto.Id,
            IsActive = appointmentTypeDto.IsActive,
            Name = appointmentTypeDto.Name
        };
    }
}