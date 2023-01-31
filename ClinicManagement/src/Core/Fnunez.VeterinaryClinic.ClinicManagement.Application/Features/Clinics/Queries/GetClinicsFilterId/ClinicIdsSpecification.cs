using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterId;

public class ClinicIdsSpecification : BaseSpecification<Clinic, string>
{
    public ClinicIdsSpecification(string idFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(c => c.IsActive)
            .Where(c => c.Id.ToString().Contains(
                idFilterValue.Trim().ToLower()))
            .Take(10);

        Query
            .Select(c => $"{c.Id}");
    }
}