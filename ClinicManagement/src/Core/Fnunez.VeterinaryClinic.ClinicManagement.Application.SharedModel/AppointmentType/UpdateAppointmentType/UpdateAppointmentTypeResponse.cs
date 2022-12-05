using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.UpdateAppointmentType;

public class UpdateAppointmentTypeResponse : BaseResponse
{
    public AppointmentTypeDto AppointmentType { get; set; } = new AppointmentTypeDto();

    public UpdateAppointmentTypeResponse(Guid correlationId) : base(correlationId)
    {
    }
}