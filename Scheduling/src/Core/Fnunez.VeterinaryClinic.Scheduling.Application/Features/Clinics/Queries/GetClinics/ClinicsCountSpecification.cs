using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinics;

public class ClinicsCountSpecification : BaseSpecification<Clinic>
{
    public ClinicsCountSpecification(GetClinicsRequest request)
    {
        Query.AsNoTracking();

        ApplyFilterAddress(request);

        ApplyFilterEmailAddress(request);

        ApplyFilterId(request);

        ApplyFilterName(request);

        ApplyFilterSearch(request);
    }

    private void ApplyFilterAddress(GetClinicsRequest request)
    {
        if (string.IsNullOrEmpty(request.AddressFilterValue))
            return;

        string addressFilterValue = request.AddressFilterValue
            .Trim().ToLower();

        Query
            .Where(c => c.Address.Trim().ToLower().Contains(
                addressFilterValue));
    }

    private void ApplyFilterEmailAddress(GetClinicsRequest request)
    {
        if (string.IsNullOrEmpty(request.EmailAddressFilterValue))
            return;

        string emailAddressFilterValue = request.EmailAddressFilterValue
            .Trim().ToLower();

        Query
            .Where(c => c.EmailAddress.Trim().ToLower().Contains(
                emailAddressFilterValue));
    }

    private void ApplyFilterId(GetClinicsRequest request)
    {
        if (string.IsNullOrEmpty(request.IdFilterValue))
            return;

        string idFilterValue = request.IdFilterValue.Trim();

        Query
            .Where(c => c.Id.ToString().Contains(idFilterValue));
    }

    private void ApplyFilterName(GetClinicsRequest request)
    {
        if (string.IsNullOrEmpty(request.NameFilterValue))
            return;

        string nameFilterValue = request.NameFilterValue.Trim().ToLower();

        Query
            .Where(c => c.Name.Trim().ToLower().Contains(nameFilterValue));
    }

    private void ApplyFilterSearch(GetClinicsRequest request)
    {
        if (string.IsNullOrEmpty(request.SearchFilterValue))
            return;

        string searchFilterValue = request.SearchFilterValue.Trim().ToLower();

        Query
            .Search(c => c.Address, $"%{searchFilterValue}%")
            .Search(c => c.EmailAddress, $"%{searchFilterValue}%")
            .Search(c => c.Id.ToString(), $"%{searchFilterValue}%")
            .Search(c => c.Name, $"%{searchFilterValue}%");
    }
}