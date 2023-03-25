using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;

public class Appointment : BaseAuditableEntity<Guid>, IAggregateRoot
{
    public DateTimeOffset? ConfirmOn { get; private set; }
    public DateTimeOffsetRange DateRange { get; private set; } = null!;
    public string Description { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;

    public int AppointmentTypeId { get; private set; }
    public int ClientId { get; private set; }
    public int ClinicId { get; private set; }
    public int DoctorId { get; private set; }
    public int PatientId { get; private set; }
    public int RoomId { get; private set; }

    #region Navigations
    public AppointmentType AppointmentType { get; private set; } = null!;
    public Client Client { get; private set; } = null!;
    public Clinic Clinic { get; private set; } = null!;
    public Doctor Doctor { get; private set; } = null!;
    public Patient Patient { get; private set; } = null!;
    public Room Room { get; private set; } = null!;
    #endregion

    public Appointment()
    {
    }

    public Appointment(
        Guid id,
        int appointmentTypeId,
        int clientId,
        int clinicId,
        int doctorId,
        int patientId,
        int roomId,
        DateTimeOffsetRange dateRange,
        string description,
        string title,
        DateTimeOffset? confirmOn = null)
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

        if (clinicId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(clinicId)} cannot be zero or negative.",
                nameof(clinicId));

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

        if (dateRange is null)
            throw new ArgumentNullException(nameof(dateRange));

        if (string.IsNullOrEmpty(description))
            throw new ArgumentException(
                $"Required input {nameof(description)} was empty.",
                nameof(description));

        if (string.IsNullOrEmpty(title))
            throw new ArgumentException(
                $"Required input {nameof(title)} was empty.",
                nameof(title));

        Id = id;
        AppointmentTypeId = appointmentTypeId;
        ClientId = clientId;
        ClinicId = clinicId;
        DoctorId = doctorId;
        PatientId = patientId;
        RoomId = roomId;
        DateRange = dateRange;
        Description = description;
        Title = title;
        ConfirmOn = confirmOn;
    }

    public void UpdateAppointmentType(int appointmentTypeId)
    {
        if (appointmentTypeId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(appointmentTypeId)} cannot be zero or negative.",
                nameof(appointmentTypeId));

        AppointmentTypeId = appointmentTypeId;
    }

    public void UpdateConfirmOn(DateTimeOffset confirmOn)
    {
        if (ConfirmOn.HasValue)
            return;

        ConfirmOn = confirmOn;
    }

    public void UpdateDateRange(DateTimeOffsetRange dateRange)
    {
        if (dateRange is null)
            throw new ArgumentNullException(nameof(dateRange));

        DateRange = dateRange;
    }

    public void UpdateDescription(string description)
    {
        if (string.IsNullOrEmpty(description))
            throw new ArgumentException(
                $"Required input {nameof(description)} was empty.",
                nameof(description));

        Description = description;
    }

    public void UpdateDoctor(int doctorId)
    {
        if (doctorId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(doctorId)} cannot be zero or negative.",
                nameof(doctorId));

        DoctorId = doctorId;
    }

    public void UpdateRoom(int roomId)
    {
        if (roomId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(roomId)} cannot be zero or negative.",
                nameof(roomId));

        RoomId = roomId;
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