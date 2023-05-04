using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterCode;

public class GetAppointmentTypesFilterCodeRequest : BaseRequest
{
    public string CodeFilterValue { get; set; } = null!;
}