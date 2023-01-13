using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterName;

public class GetAppointmentTypesFilterNameRequest : BaseRequest
{
    public string NameFilterValue { get; set; } = null!;
}