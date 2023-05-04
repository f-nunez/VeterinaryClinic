using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;

public class CreateAppointmentTypeResponse : BaseResponse
{
    public AppointmentTypeDto AppointmentType { get; set; } = new AppointmentTypeDto();

    public CreateAppointmentTypeResponse(Guid correlationId) : base(correlationId)
    {
    }
}