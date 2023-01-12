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
            .Include(a => a.AppointmentType)
            .Include(a => a.Client)
            .Include(a => a.Clinic)
            .Include(a => a.Doctor)
            .Include(a => a.Patient);

        int clientId = 0;
        int.TryParse(request.ClientIdFilterValue, out clientId);

        if (clientId > 0)
            Query.Where(a => a.ClientId == clientId);

        int clinicId = 0;
        int.TryParse(request.ClinicIdFilterValue, out clinicId);

        if (clinicId > 0)
            Query.Where(a => a.ClinicId == clinicId);

        int patientId = 0;
        int.TryParse(request.PatientIdFilterValue, out patientId);

        if (patientId > 0)
            Query.Where(a => a.PatientId == patientId);

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