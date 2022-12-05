using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;

public class DeleteAppointmentTypeResponse : BaseResponse
{
    public DeleteAppointmentTypeResponse(Guid correlationId) : base(correlationId)
    {
    }
}