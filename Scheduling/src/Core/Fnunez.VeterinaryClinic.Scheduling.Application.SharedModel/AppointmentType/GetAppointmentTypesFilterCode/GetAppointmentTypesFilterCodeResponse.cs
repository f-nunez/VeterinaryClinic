using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterCode;

public class GetAppointmentTypesFilterCodeResponse : BaseResponse
{
    public List<string> AppointemntTypeCodes { get; set; } = new();

    public GetAppointmentTypesFilterCodeResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}