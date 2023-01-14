using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.AppointmentTypes;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;

public static class AppointmentTypeHelper
{
    public static AppointmentTypeVm MapAppointmentTypeViewModel(
        AppointmentTypeDto appointmentType)
    {
        return new AppointmentTypeVm
        {
            Code = appointmentType.Code,
            Duration = appointmentType.Duration,
            Id = appointmentType.Id,
            Name = appointmentType.Name
        };
    }
}