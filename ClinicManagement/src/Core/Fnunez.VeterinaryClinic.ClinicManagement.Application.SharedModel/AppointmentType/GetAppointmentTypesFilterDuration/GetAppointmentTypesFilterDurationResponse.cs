using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterDuration;

public class GetAppointmentTypesFilterDurationResponse : BaseResponse
{
    public List<string> AppointmentTypeDurations { get; set; } = new();

    public GetAppointmentTypesFilterDurationResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}