using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorsFilterFullName;

public class DoctorFullNamesSpecification : BaseSpecification<Doctor, string>
{
    public DoctorFullNamesSpecification(string fullNameFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(d => d.IsActive)
            .Where(d => d.FullName.Trim().ToLower().Contains(
                fullNameFilterValue.Trim().ToLower()))
            .OrderBy(d => d.FullName)
            .Take(10);

        Query
            .Select(d => d.FullName);
    }
}