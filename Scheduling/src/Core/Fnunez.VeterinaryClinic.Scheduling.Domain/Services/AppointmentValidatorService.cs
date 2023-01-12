using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.Services;

public static class AppointmentValidatorService
{
    private const string ErrorMessage = "Required duration is not covered in Appointment date range between ({0}) and ({1}). AppointmentType: ({2}).";

    public static void ValidateDuration(
        Appointment appointment,
        AppointmentType appointmentType)
    {
        DateTimeOffset requiredTime = appointment.DateRange.StartOn
            .AddMinutes(appointmentType.Duration);

        var comparer = Comparer<DateTimeOffset>.Default;

        if (comparer.Compare(requiredTime, appointment.DateRange.EndOn) > 0)
            throw new ArgumentException(
                string.Format(
                    ErrorMessage,
                    appointment.DateRange.StartOn,
                    appointment.DateRange.EndOn,
                    appointmentType.ToString()
                )
            );
    }
}