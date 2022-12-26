using Fnunez.VeterinaryClinic.Scheduling.Domain.RelativeAppointmentAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.RelativeAppointmentAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.Services;

// Create schedules that cover the appointment date range
// Means that an appointment can exists between scheduleA.StartOn and scheduleB.EndOn
// So, it's necessary to create schedules for previous/current/next month
// Because Schedule represent a month per clinic
public class ScheduleService//TODO: Rename to SiblingAppointmentService
{
    private DateTimeOffsetRange GetRelativeAppointmentDateRange(
        DateTimeOffset scheduleStartOn,
        DateTimeOffset scheduleEndOn,
        DateTimeOffset appointmentStartOn,
        DateTimeOffset appointmentEndOn)
    {
        return new DateTimeOffsetRange(
            GetRelativeAppointmentStartOn(scheduleStartOn, appointmentStartOn),
            GetRelativeAppointmentStartOn(scheduleEndOn, appointmentEndOn)
        );
    }

    private DateTimeOffset GetRelativeAppointmentStartOn(
        DateTimeOffset scheduleStartOn,
        DateTimeOffset appointmentStartOn)
    {
        if (scheduleStartOn < appointmentStartOn)
            return appointmentStartOn;

        if (scheduleStartOn == appointmentStartOn)
            return appointmentStartOn;

        if (scheduleStartOn > appointmentStartOn)
            return scheduleStartOn;

        throw new ArgumentException($"Not matched time {nameof(scheduleStartOn)} :({scheduleStartOn}) and {nameof(appointmentStartOn)} ({appointmentStartOn}).");
    }

    private DateTimeOffset GetRelativeAppointmentEndOn(
        DateTimeOffset scheduleEndOn,
        DateTimeOffset appointmentEndOn)
    {
        if (scheduleEndOn > appointmentEndOn)
            return appointmentEndOn;

        if (scheduleEndOn == appointmentEndOn)
            return appointmentEndOn;

        if (scheduleEndOn < appointmentEndOn)
            return scheduleEndOn;

        throw new ArgumentException($"Not matched time {nameof(scheduleEndOn)} :({scheduleEndOn}) and {nameof(appointmentEndOn)} ({appointmentEndOn}).");
    }

    private IEnumerable<DateTime> EachMonth(
        DateTime appointmentStartOn,
        DateTime appointmentEndOn)
    {
        var monthOfStartOn = FirstDayOfMonth(appointmentStartOn);
        var monthOfEndOn = FirstDayOfMonth(appointmentEndOn);

        for (var month = monthOfStartOn;
            month.Date <= monthOfEndOn;
            month = month.AddMonths(1))
            yield return month;
    }

    private DateTime StartOfDay(DateTime dateTime)
    {
        return dateTime.Date;
    }

    private DateTime EndOfDay(DateTime dateTime)
    {
        return dateTime.Date.AddDays(1).AddTicks(-1);
    }

    private DateTime FirstDayOfMonth(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, dateTime.Kind);
    }

    private DateTime LastDayOfMonth(DateTime dateTime)
    {
        int daysOfMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        return new DateTime(dateTime.Year, dateTime.Month, daysOfMonth, 0, 0, 0, dateTime.Kind);
    }
}