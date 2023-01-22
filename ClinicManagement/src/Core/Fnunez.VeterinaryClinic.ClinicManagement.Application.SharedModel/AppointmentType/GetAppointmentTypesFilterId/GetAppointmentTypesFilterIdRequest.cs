using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterId;

public class GetAppointmentTypesFilterIdRequest : BaseRequest
{
    public string IdFilterValue { get; set; } = null!;
}