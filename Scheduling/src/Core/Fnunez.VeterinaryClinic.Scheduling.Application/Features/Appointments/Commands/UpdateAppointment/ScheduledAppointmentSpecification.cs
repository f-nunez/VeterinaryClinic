using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.UpdateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.UpdateAppointment;

public class ScheduledAppointmentSpecification : BaseSpecification<Appointment>
{
    public ScheduledAppointmentSpecification(
        UpdateAppointmentRequest request,
        int clientId,
        int patientId)
    {
        Query
            .AsNoTracking()
            .Where(a => a.Id != request.AppointmentId)
            .Where(a => a.ClientId == clientId)
            .Where(a => a.PatientId == patientId)
            .Where(a =>
                (//Cover range dates
                    a.DateRange.StartOn >= request.StartOn &&
                    a.DateRange.StartOn <= request.EndOn &&
                    a.DateRange.EndOn >= request.StartOn &&
                    a.DateRange.EndOn <= request.EndOn
                )
                ||
                (//Cover fully partial dates between start and end
                    a.DateRange.StartOn <= request.StartOn &&
                    a.DateRange.EndOn >= request.EndOn
                )
                ||
                (//Cover partial early start dates until end dates
                    a.DateRange.StartOn <= request.StartOn &&
                    a.DateRange.EndOn > request.StartOn &&
                    a.DateRange.EndOn <= request.EndOn
                )
                ||
                (//Cover from start dates to partial late end dates
                    a.DateRange.StartOn >= request.StartOn &&
                    a.DateRange.StartOn < request.EndOn &&
                    a.DateRange.EndOn >= request.EndOn
                )
            );
    }
}