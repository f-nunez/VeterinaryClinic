using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.CreateAppointment;

public class ScheduledAppointmentSpecification : BaseSpecification<Appointment>
{
    public ScheduledAppointmentSpecification(CreateAppointmentRequest request)
    {
        Query
            .AsNoTracking()
            .Where(a => a.AppointmentTypeId == request.AppointmentTypeId)
            .Where(a => a.ClientId == request.ClientId)
            .Where(a => a.PatientId == request.PatientId)
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
                    a.DateRange.EndOn >= request.StartOn &&
                    a.DateRange.EndOn >= request.EndOn
                )
                ||
                (//Cover from start dates to partial late end dates
                    a.DateRange.StartOn >= request.StartOn &&
                    a.DateRange.StartOn <= request.EndOn &&
                    a.DateRange.EndOn >= request.EndOn
                )
            );
    }
}