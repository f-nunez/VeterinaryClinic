using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterName;

public class AppointmentTypeNamesSpecification
    : BaseSpecification<AppointmentType, string>
{
    public AppointmentTypeNamesSpecification(string nameFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(at => at.Name.Trim().ToLower().Contains(
                nameFilterValue.Trim().ToLower()))
            .OrderBy(at => at.Name)
            .Take(10);

        Query
            .Select(at => at.Name);
    }
}