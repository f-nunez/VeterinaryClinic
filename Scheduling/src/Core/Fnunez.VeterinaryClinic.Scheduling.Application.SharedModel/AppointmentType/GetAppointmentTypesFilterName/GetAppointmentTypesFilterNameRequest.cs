using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterName;

public class GetAppointmentTypesFilterNameRequest : BaseRequest
{
    public string NameFilterValue { get; set; } = null!;
}