using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentDetail;

public class AppointmentsSpecification : BaseSpecification<Appointment>
{
    public AppointmentsSpecification(GetAppointmentsRequest request)
    {
        Query
            .AsNoTracking()
            .Where(a => a.IsActive)
            .Include(a => a.AppointmentType)
            .Include(a => a.Client)
            .Include(a => a.Clinic)
            .Include(a => a.Doctor)
            .Include(a => a.Patient);

        if (request.ClientId > 0)
            Query.Where(a => a.ClientId == request.ClientId);

        if (request.ClinicId > 0)
            Query.Where(a => a.ClinicId == request.ClinicId);

        if (request.PatientId > 0)
            Query.Where(a => a.PatientId == request.PatientId);

        Query
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

        Query.OrderBy(a => a.DateRange.StartOn);
    }
}