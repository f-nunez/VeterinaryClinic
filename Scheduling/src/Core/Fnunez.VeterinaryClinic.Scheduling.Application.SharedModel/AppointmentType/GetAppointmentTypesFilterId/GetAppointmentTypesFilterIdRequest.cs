using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterId;

public class GetAppointmentTypesFilterIdRequest : BaseRequest
{
    public string IdFilterValue { get; set; } = null!;
}