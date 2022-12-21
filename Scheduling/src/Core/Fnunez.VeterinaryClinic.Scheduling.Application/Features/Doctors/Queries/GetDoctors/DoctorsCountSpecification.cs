using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctors;

public class DoctorsCountSpecification : BaseSpecification<Doctor>
{
    public DoctorsCountSpecification(GetDoctorsRequest request)
    {
        Query.AsNoTracking();

        ApplyFilterFullName(request);

        ApplyFilterId(request);

        ApplyFilterSearch(request);
    }

    private void ApplyFilterFullName(GetDoctorsRequest request)
    {
        if (string.IsNullOrEmpty(request.FullNameFilterValue))
            return;

        string fullNameFilterValue = request.FullNameFilterValue
            .Trim().ToLower();

        Query
            .Where(d => d.FullName.Trim().ToLower().Contains(
                fullNameFilterValue));
    }

    private void ApplyFilterId(GetDoctorsRequest request)
    {
        if (string.IsNullOrEmpty(request.IdFilterValue))
            return;

        string idFilterValue = request.IdFilterValue.Trim();

        Query
            .Where(d => d.Id.ToString().Contains(idFilterValue));
    }

    private void ApplyFilterSearch(GetDoctorsRequest request)
    {
        if (string.IsNullOrEmpty(request.SearchFilterValue))
            return;

        string searchFilterValue = request.SearchFilterValue.Trim().ToLower();

        Query
            .Search(d => d.Id.ToString(), $"%{searchFilterValue}%")
            .Search(d => d.FullName, $"%{searchFilterValue}%");
    }
}