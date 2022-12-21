using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterCode;

public class GetAppointmentTypesFilterCodeRequest : BaseRequest
{
    public string CodeFilterValue { get; set; } = null!;
}