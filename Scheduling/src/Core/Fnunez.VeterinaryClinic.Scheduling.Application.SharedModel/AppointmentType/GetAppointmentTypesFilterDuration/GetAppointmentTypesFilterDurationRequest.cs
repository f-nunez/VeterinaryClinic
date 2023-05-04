using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterDuration;

public class GetAppointmentTypesFilterDurationRequest : BaseRequest
{
    public string DurationFilterValue { get; set; } = null!;
}