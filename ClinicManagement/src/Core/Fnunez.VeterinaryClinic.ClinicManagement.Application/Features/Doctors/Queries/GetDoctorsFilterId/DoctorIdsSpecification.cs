using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorsFilterId;

public class DoctorIdsSpecification : BaseSpecification<Doctor, string>
{
    public DoctorIdsSpecification(string idFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(d => d.IsActive)
            .Where(d => d.Id.ToString().Contains(idFilterValue.Trim()))
            .OrderBy(d => d.Id)
            .Take(10);

        Query
            .Select(d => $"{d.Id}");
    }
}