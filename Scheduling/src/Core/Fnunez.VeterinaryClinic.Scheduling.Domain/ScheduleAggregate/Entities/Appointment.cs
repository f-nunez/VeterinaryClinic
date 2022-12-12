using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.Entities;

public class Appointment : BaseEntity<Guid>
{
    public int AppointmentTypeId { get; private set; }
    public int ClientId { get; private set; }
    public int DoctorId { get; private set; }
    public int PatientId { get; private set; }
    public int RoomId { get; private set; }
    public Guid ScheduleId { get; private set; }
    public DateTimeOffsetRange DateRange { get; private set; } = null!;
    public string Title { get; private set; }
    public DateTimeOffset? ConfirmOn { get; private set; }
    public bool IsPotentiallyConflicting { get; private set; }

    public AppointmentType AppointmentType { get; set; } = null!;
    public Client Client { get; private set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public Patient Patient { get; private set; } = null!;
    public Room Room  { get; set; } = null!;

    public Appointment()
    {
        Title = string.Empty;
    }

    public Appointment(
        Guid id,
        int appointmentTypeId,
        int clientId,
        int doctorId,
        int patientId,
        int roomId,
        Guid scheduleId,
        DateTimeOffsetRange dateRange,
        string title,
        DateTime? confirmOn = null)
    {
        if (id == Guid.Empty)
            throw new ArgumentException(
                $"Required input {nameof(id)} cannot be empty.",
                nameof(id));

        if (appointmentTypeId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(appointmentTypeId)} cannot be zero or negative.",
                nameof(appointmentTypeId));

        if (clientId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(clientId)} cannot be zero or negative.",
                nameof(clientId));

        if (doctorId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(doctorId)} cannot be zero or negative.",
                nameof(doctorId));

        if (patientId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(patientId)} cannot be zero or negative.",
                nameof(patientId));

        if (roomId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(roomId)} cannot be zero or negative.",
                nameof(roomId));

        if (scheduleId == Guid.Empty)
            throw new ArgumentException(
                $"Required input {nameof(scheduleId)} cannot be empty.",
                nameof(scheduleId));

        if (dateRange is null)
            throw new ArgumentNullException(nameof(dateRange));

        if (string.IsNullOrEmpty(title))
            throw new ArgumentException(
                $"Required input {nameof(title)} was empty.",
                nameof(title));

        Id = id;
        AppointmentTypeId = appointmentTypeId;
        ClientId = clientId;
        DoctorId = doctorId;
        PatientId = patientId;
        RoomId = roomId;
        ScheduleId = scheduleId;
        DateRange = dateRange;
        Title = title;
        ConfirmOn = confirmOn;
    }

    public void UpdateAppointmentType(AppointmentType appointmentType)
    {
        if (appointmentType is null)
            throw new ArgumentNullException(nameof(appointmentType));

        AppointmentTypeId = appointmentType.Id;

        DateRange = DateRange.CreateNewEndOn(
          DateRange.StartOn.AddMinutes(appointmentType.Duration));
    }

    public void UpdateConfirm(DateTimeOffset confirmOn)
    {
        if (ConfirmOn.HasValue)
            return;

        ConfirmOn = confirmOn;
    }

    public void UpdateDoctor(int doctorId)
    {
        if (doctorId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(doctorId)} cannot be zero or negative.",
                nameof(doctorId));

        DoctorId = doctorId;
    }

    public void UpdateIsPotentiallyConflicting(bool value)
    {
        IsPotentiallyConflicting = value;
    }

    public void UpdateRoom(int roomId)
    {
        if (roomId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(roomId)} cannot be zero or negative.",
                nameof(roomId));

        RoomId = roomId;
    }

    public void UpdateStartOn(DateTimeOffset startOn)
    {
        DateRange = new DateTimeOffsetRange(
          startOn,
          TimeSpan.FromMinutes(DateRange.DurationInMinutes()));
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentException(
                $"Required input {nameof(title)} was empty.",
                nameof(title));

        Title = title;
    }
}