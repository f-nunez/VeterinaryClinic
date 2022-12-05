using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypeById;

public class GetAppointmentTypeByIdRequest : BaseRequest
{
    public int Id { get; set; }
}