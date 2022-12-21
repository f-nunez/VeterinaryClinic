using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypes;

public class AppointmentTypesCountSpecification
    : BaseSpecification<AppointmentType>
{
    public AppointmentTypesCountSpecification(GetAppointmentTypesRequest request)
    {
        Query.AsNoTracking();

        ApplyFilterCode(request);

        ApplyFilterDuration(request);

        ApplyFilterId(request);

        ApplyFilterName(request);

        ApplyFilterSearch(request);
    }

    private void ApplyFilterCode(GetAppointmentTypesRequest request)
    {
        if (string.IsNullOrEmpty(request.CodeFilterValue))
            return;

        string codeFilterValue = request.CodeFilterValue.Trim().ToLower();

        Query
            .Where(at => at.Code.Trim().ToLower().Contains(codeFilterValue));
    }

    private void ApplyFilterDuration(GetAppointmentTypesRequest request)
    {
        if (string.IsNullOrEmpty(request.DurationFilterValue))
            return;

        string durationFilterValue = request.DurationFilterValue
            .Trim().ToLower();

        Query
            .Where(at => at.Duration.ToString().Contains(durationFilterValue));
    }

    private void ApplyFilterId(GetAppointmentTypesRequest request)
    {
        if (string.IsNullOrEmpty(request.IdFilterValue))
            return;

        string idFilterValue = request.IdFilterValue.Trim();

        Query
            .Where(at => at.Id.ToString().Contains(idFilterValue));
    }

    private void ApplyFilterName(GetAppointmentTypesRequest request)
    {
        if (string.IsNullOrEmpty(request.NameFilterValue))
            return;

        string nameFilterValue = request.NameFilterValue.Trim().ToLower();

        Query
            .Where(at => at.Name.Trim().ToLower().Contains(nameFilterValue));
    }

    private void ApplyFilterSearch(GetAppointmentTypesRequest request)
    {
        if (string.IsNullOrEmpty(request.SearchFilterValue))
            return;

        string searchFilterValue = request.SearchFilterValue.Trim().ToLower();

        Query
            .Search(at => at.Code, $"%{searchFilterValue}%")
            .Search(at => at.Duration.ToString(), $"%{searchFilterValue}%")
            .Search(at => at.Id.ToString(), $"%{searchFilterValue}%")
            .Search(at => at.Name, $"%{searchFilterValue}%");
    }
}