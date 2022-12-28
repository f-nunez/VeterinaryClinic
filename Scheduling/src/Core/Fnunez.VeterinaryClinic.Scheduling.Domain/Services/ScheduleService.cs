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
    // Create Schedules that do not exists in previous/current/next months
    // for Appointment's date range
    public (RelativeAppointment RelativeAppointmentToAdd, List<Schedule> SchedulesToAdd)
    GetScheduleBasedOnNewRelativeAppointment(
        int clinicId,
        Appointment appointmentToSchedule)
    {
        DateTimeOffset startOn = appointmentToSchedule.DateRange.StartOn;
        DateTimeOffset endOn = appointmentToSchedule.DateRange.EndOn;

        if (startOn >= endOn)
            throw new AppointmentDateRangeException(startOn, endOn);

        IEnumerable<DateTime> expectedMonths = EachMonth(
            startOn.UtcDateTime,
            endOn.UtcDateTime
        );

        var relativeAppointment = new RelativeAppointment(Guid.NewGuid());
        List<Schedule> newSchedules = new();

        foreach (var expectedMonth in expectedMonths)
        {
            var newSchedule = MapNewSchedule(clinicId, expectedMonth);
            var newAppointment = MapNewAppointment(relativeAppointment.Id, appointmentToSchedule, newSchedule);
            newSchedule.AddAppointment(newAppointment);
            newSchedules.Add(newSchedule);

            var relativeAppointmentItem = new RelativeAppointmentItem(
                Guid.NewGuid(),
                relativeAppointment.Id,
                newAppointment.Id,
                newSchedule.Id
            );

            relativeAppointment.AddSiblingAppointment(relativeAppointmentItem);
        }

        return (relativeAppointment, newSchedules);
    }

    // Create/Update Schedules to cover appointment's date range in previous/current/next months
    public (RelativeAppointment RelativeAppointmentToAdd, List<Schedule> SchedulesToCreate, List<Schedule> SchedulesToUpdate)
    GetScheduleBasedOnNewRelativeAppointment(
        int clinicId,
        List<Schedule> currentSchedules,
        Appointment appointmentToSchedule)
    {
        DateTimeOffset startOn = appointmentToSchedule.DateRange.StartOn;
        DateTimeOffset endOn = appointmentToSchedule.DateRange.EndOn;

        if (startOn >= endOn)
            throw new AppointmentDateRangeException(startOn, endOn);

        currentSchedules = currentSchedules
            .OrderBy(s => s.DateRange.StartOn).ToList();

        var expectedgMonths = EachMonth(startOn.UtcDateTime, endOn.UtcDateTime)
            .ToList();

        int expectedgMonthsCounter = expectedgMonths.Count();

        DateTime expectedMonthStartOn;
        DateTime expectedMonthEndOn;
        var relativeAppointment = new RelativeAppointment(Guid.NewGuid());
        List<Schedule> newSchedules = new();

        for (int i = 0; i < expectedgMonthsCounter; i++)
        {
            expectedMonthStartOn = expectedgMonths[i];
            expectedMonthEndOn = EndOfDay(LastDayOfMonth(expectedMonthStartOn));

            Schedule? currentScheduleInExpectedMonth = currentSchedules
                .FirstOrDefault(s => s.DateRange.StartOn == expectedgMonths[i]);

            if (currentScheduleInExpectedMonth != null)
            {
                var newAppointment = MapNewAppointment(relativeAppointment.Id, appointmentToSchedule, currentScheduleInExpectedMonth);
                currentScheduleInExpectedMonth.AddAppointment(newAppointment);

                var relativeAppointmentItem = new RelativeAppointmentItem(
                    Guid.NewGuid(),
                    relativeAppointment.Id,
                    newAppointment.Id,
                    currentScheduleInExpectedMonth.Id
                );

                relativeAppointment.AddSiblingAppointment(relativeAppointmentItem);
            }
            else
            {
                var newSchedule = MapNewSchedule(clinicId, expectedMonthStartOn);
                var newAppointment = MapNewAppointment(relativeAppointment.Id, appointmentToSchedule, newSchedule);
                newSchedule.AddAppointment(newAppointment);
                newSchedules.Add(newSchedule);

                var relativeAppointmentItem = new RelativeAppointmentItem(
                    Guid.NewGuid(),
                    relativeAppointment.Id,
                    newAppointment.Id,
                    newSchedule.Id
                );

                relativeAppointment.AddSiblingAppointment(relativeAppointmentItem);
            }
        }

        return (relativeAppointment, newSchedules, currentSchedules);
    }

    // Create/Update Schedules to cover appointment's date range when exists a relative appointment collection
    public (RelativeAppointment RelativeAppointmentToAdd, List<Schedule> SchedulesToCreate, List<Schedule> SchedulesToUpdate)
    GetScheduleBasedOnCurrentRelativeAppointment(
        int clinicId,
        List<Schedule> currentSchedules,
        Guid currentScheduleId,
        Guid currentAppointmentId,
        Appointment appointmentToSchedule)
    {
        DateTimeOffset startOn = appointmentToSchedule.DateRange.StartOn;
        DateTimeOffset endOn = appointmentToSchedule.DateRange.EndOn;

        if (startOn >= endOn)
            throw new AppointmentDateRangeException(startOn, endOn);

        currentSchedules = currentSchedules
            .OrderBy(s => s.DateRange.StartOn).ToList();

        var expectedgMonths = EachMonth(startOn.UtcDateTime, endOn.UtcDateTime)
            .ToList();

        int expectedgMonthsCounter = expectedgMonths.Count();

        DateTime expectedMonthStartOn;
        DateTime expectedMonthEndOn;
        var relativeAppointment = new RelativeAppointment(Guid.NewGuid());
        List<Schedule> newSchedules = new();

        for (int i = 0; i < expectedgMonthsCounter; i++)
        {
            expectedMonthStartOn = expectedgMonths[i];
            expectedMonthEndOn = EndOfDay(LastDayOfMonth(expectedMonthStartOn));

            Schedule? currentScheduleInExpectedMonth = currentSchedules
                .FirstOrDefault(s => s.DateRange.StartOn == expectedgMonths[i]);

            if (currentScheduleInExpectedMonth != null)
            {
                if (currentScheduleId == currentScheduleInExpectedMonth.Id)
                {
                    var currentAppointment = currentScheduleInExpectedMonth
                    .Appointments
                    .FirstOrDefault(a => a.Id == currentAppointmentId);

                    if (currentAppointment is null)
                        throw new ArgumentException($"Not found appointment id: {currentAppointmentId} in schedule id: {currentScheduleId}");

                    var appointmentDateRange = GetRelativeAppointmentDateRange(
                        expectedMonthStartOn,
                        expectedMonthEndOn,
                        appointmentToSchedule.DateRange.StartOn,
                        appointmentToSchedule.DateRange.EndOn
                    );

                    currentAppointment.UpdateDateRange(appointmentDateRange);
                    currentAppointment.AssignRelativeAppointment(relativeAppointment.Id);
                }
                else
                {
                    var newAppointment = MapNewAppointment(relativeAppointment.Id, appointmentToSchedule, currentScheduleInExpectedMonth);
                    currentScheduleInExpectedMonth.AddAppointment(newAppointment);

                    var relativeAppointmentItem = new RelativeAppointmentItem(
                        Guid.NewGuid(),
                        relativeAppointment.Id,
                        newAppointment.Id,
                        currentScheduleInExpectedMonth.Id
                    );

                    relativeAppointment.AddSiblingAppointment(relativeAppointmentItem);
                }
            }
            else
            {
                var newSchedule = MapNewSchedule(clinicId, expectedMonthStartOn);
                var newAppointment = MapNewAppointment(relativeAppointment.Id, appointmentToSchedule, newSchedule);
                newSchedule.AddAppointment(newAppointment);
                newSchedules.Add(newSchedule);

                var relativeAppointmentItem = new RelativeAppointmentItem(
                    Guid.NewGuid(),
                    relativeAppointment.Id,
                    newAppointment.Id,
                    newSchedule.Id
                );

                relativeAppointment.AddSiblingAppointment(relativeAppointmentItem);
            }
        }

        return (relativeAppointment, newSchedules, currentSchedules);
    }

    private Schedule MapNewSchedule(
        int clinicId,
        DateTime appointmentStartOn)
    {
        var startOn = FirstDayOfMonth(appointmentStartOn);
        var lastDayOfMonth = LastDayOfMonth(startOn);
        var endOn = EndOfDay(lastDayOfMonth);
        var dateRange = new DateTimeOffsetRange(startOn, endOn);

        return new Schedule(
            Guid.NewGuid(),
            clinicId,
            dateRange
        );
    }

    private Appointment MapNewAppointment(
        Guid appointerId,
        Appointment appointment,
        Schedule schedule)
    {
        var dateRange = GetRelativeAppointmentDateRange(
            schedule.DateRange.StartOn,
            schedule.DateRange.EndOn,
            appointment.DateRange.StartOn,
            appointment.DateRange.EndOn
        );

        var appointerment = new Appointment(
            Guid.NewGuid(),
            appointment.AppointmentTypeId,
            appointment.ClientId,
            appointment.DoctorId,
            appointment.PatientId,
            appointment.RoomId,
            schedule.Id,
            dateRange,
            appointment.Title
        );

        appointerment.AssignRelativeAppointment(appointerId);

        return appointerment;
    }

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