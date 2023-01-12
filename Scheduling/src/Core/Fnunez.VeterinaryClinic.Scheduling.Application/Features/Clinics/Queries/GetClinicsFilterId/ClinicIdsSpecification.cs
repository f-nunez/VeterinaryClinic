using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterId;

public class ClinicIdsSpecification : BaseSpecification<Clinic, string>
{
    public ClinicIdsSpecification(string idFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(c => c.Id.ToString().Contains(
                idFilterValue.Trim().ToLower()))
            .Take(10);

        Query
            .Select(c => $"{c.Id}");
    }
}