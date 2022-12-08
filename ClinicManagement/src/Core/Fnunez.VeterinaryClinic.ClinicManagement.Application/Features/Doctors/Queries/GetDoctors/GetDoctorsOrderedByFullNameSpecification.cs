using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctors;

public class GetDoctorsOrderedByFullNameSpecification : BaseSpecification<Doctor>
{
    public GetDoctorsOrderedByFullNameSpecification()
    {
        Query.OrderBy(doctor => doctor.FullName);
    }
}