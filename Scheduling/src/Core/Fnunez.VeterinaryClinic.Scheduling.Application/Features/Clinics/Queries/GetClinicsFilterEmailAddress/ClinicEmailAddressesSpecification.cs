using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterEmailAddress;

public class ClinicEmailAddressesSpecification
    : BaseSpecification<Clinic, string>
{
    public ClinicEmailAddressesSpecification(string emailAddressFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(c => c.EmailAddress.Trim().ToLower().Contains(
                emailAddressFilterValue.Trim().ToLower()))
            .Take(10);

        Query
            .Select(c => c.EmailAddress);
    }
}