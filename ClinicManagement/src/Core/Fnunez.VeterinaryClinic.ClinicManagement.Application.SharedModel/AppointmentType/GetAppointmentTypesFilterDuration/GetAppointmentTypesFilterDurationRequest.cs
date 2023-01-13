using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterDuration;

public class GetAppointmentTypesFilterDurationRequest : BaseRequest
{
    public string DurationFilterValue { get; set; } = null!;
}