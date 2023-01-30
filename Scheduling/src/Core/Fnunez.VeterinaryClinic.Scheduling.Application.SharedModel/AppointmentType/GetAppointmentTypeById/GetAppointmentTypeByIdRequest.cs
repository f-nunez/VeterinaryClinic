using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypeById;

public class GetAppointmentTypeByIdRequest : BaseRequest
{
    public int Id { get; set; }
}