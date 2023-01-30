using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.ViewModels.AppointmentTypes;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Helpers;

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
            Name = appointmentTypeDto.Name
        };
    }
}