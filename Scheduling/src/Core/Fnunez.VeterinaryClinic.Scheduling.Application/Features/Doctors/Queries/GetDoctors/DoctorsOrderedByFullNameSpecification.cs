using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctors;

public class DoctorsOrderedByFullNameSpecification : BaseSpecification<Doctor>
{
    public DoctorsOrderedByFullNameSpecification()
    {
        Query.OrderBy(doctor => doctor.FullName);
    }
}