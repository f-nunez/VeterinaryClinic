using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypeById;

public class GetAppointmentTypeByIdResponse : BaseResponse
{
    public AppointmentTypeDto AppointmentType { get; set; } = new AppointmentTypeDto();

    public GetAppointmentTypeByIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}