using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterDuration;

public class AppointmentTypeDurationsSpecification
    : BaseSpecification<AppointmentType, string>
{
    public AppointmentTypeDurationsSpecification(string durationFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(at => at.IsActive)
            .Where(at => at.Duration.ToString().Contains(durationFilterValue))
            .OrderBy(at => at.Duration)
            .Take(10);

        Query
            .Select(at => $"{at.Duration}");
    }
}