using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterId;

public class AppointmentTypeIdsSpecification
    : BaseSpecification<AppointmentType, string>
{
    public AppointmentTypeIdsSpecification(string idFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(at => at.IsActive)
            .Where(at => at.Id.ToString().Contains(idFilterValue))
            .OrderBy(at => at.Id)
            .Take(10);

        Query
            .Select(at => $"{at.Id}");
    }
}