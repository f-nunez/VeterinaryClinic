using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.Entities;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.ValueObjects;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;

public class Schedule : BaseEntity<Guid>, IAggregateRoot
{
    private IList<Appointment> _appointments = new List<Appointment>();
    public IReadOnlyList<Appointment> Appointments => _appointments.AsReadOnly();
    public int ClinicId { get; private set; }
    public DateTimeOffsetRange DateRange { get; private set; } = null!;

    public Clinic Clinic { get; set; } = null!;

    public Schedule()
    {
    }

    public Schedule(
        Guid id,
        int clinicId,
        DateTimeOffsetRange dateRange)
    {
        if (id == Guid.Empty)
            throw new ArgumentException(
                $"Required input {nameof(id)} cannot be empty.",
                nameof(id));

        if (clinicId <= 0)
            throw new ArgumentException(
                $"Required input {nameof(clinicId)} cannot be zero or negative.",
                nameof(clinicId));

        if (dateRange is null)
            throw new ArgumentNullException(nameof(dateRange));

        Id = id;
        ClinicId = clinicId;
        DateRange = dateRange;
    }

    public void AddAppointment(Appointment appointment)
    {
        if (appointment is null)
            throw new ArgumentNullException(nameof(appointment));

        if (_appointments.Any(a => a.Id == appointment.Id))
            throw new DuplicateAppointmentException(
                "Cannot add duplicate appointment to schedule.",
                nameof(appointment));

        _appointments.Add(appointment);

        MarkConflictingAppointments();
    }

    public void RemoveAppointment(Appointment appointment)
    {
        if (appointment is null)
            throw new ArgumentNullException(nameof(appointment));

        _appointments.Remove(appointment);

        MarkConflictingAppointments();
    }

    private void MarkConflictingAppointments()
    {
        foreach (var appointment in _appointments)
        {
            var overlaping = GetOverlapingAppointmentsByPatient(appointment);
            overlaping.ForEach(a => a.UpdateIsPotentiallyConflicting(true));
            appointment.UpdateIsPotentiallyConflicting(overlaping.Any());
        }
    }

    private List<Appointment> GetOverlapingAppointmentsByPatient(
        Appointment appointment)
    {
        var potentiallyConflictingAppointments = _appointments
            .Where(a =>
                a.PatientId == appointment.PatientId &&
                a.DateRange.Overlaps(appointment.DateRange) &&
                a != appointment)
            .ToList();

        return potentiallyConflictingAppointments;
    }
}