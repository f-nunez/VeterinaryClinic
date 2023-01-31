using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterName;

public class ClinicNamesSpecification : BaseSpecification<Clinic, string>
{
    public ClinicNamesSpecification(string nameFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(c => c.IsActive)
            .Where(c => c.Name.Trim().ToLower().Contains(
                nameFilterValue.Trim().ToLower()))
            .Take(10);

        Query
            .Select(c => c.Name);
    }
}