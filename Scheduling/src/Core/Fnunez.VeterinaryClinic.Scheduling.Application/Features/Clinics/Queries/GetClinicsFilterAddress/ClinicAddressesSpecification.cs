using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterAddress;

public class ClinicAddressesSpecification : BaseSpecification<Clinic, string>
{
    public ClinicAddressesSpecification(string addressFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(c => c.Address.Trim().ToLower().Contains(
                addressFilterValue.Trim().ToLower()))
            .Take(10);

        Query
            .Select(c => c.Address);
    }
}