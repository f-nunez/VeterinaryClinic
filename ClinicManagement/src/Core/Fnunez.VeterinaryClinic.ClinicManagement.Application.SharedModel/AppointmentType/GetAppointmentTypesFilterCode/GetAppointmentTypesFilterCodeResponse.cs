using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterCode;

public class GetAppointmentTypesFilterCodeResponse : BaseResponse
{
    public List<string> AppointemntTypeCodes { get; set; } = new();

    public GetAppointmentTypesFilterCodeResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}