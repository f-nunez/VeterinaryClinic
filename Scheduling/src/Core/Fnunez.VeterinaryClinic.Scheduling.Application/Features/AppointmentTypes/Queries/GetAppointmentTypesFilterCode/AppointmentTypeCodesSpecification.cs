using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterCode;

public class AppointmentTypeCodesSpecification
    : BaseSpecification<AppointmentType, string>
{
    public AppointmentTypeCodesSpecification(string codeFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(at => at.IsActive)
            .Where(at => at.Code.Trim().ToLower().Contains(
                codeFilterValue.Trim().ToLower()))
            .OrderBy(at => at.Code)
            .Take(10);

        Query
            .Select(at => at.Code);
    }
}