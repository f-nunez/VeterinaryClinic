using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;

public class DeleteAppointmentTypeRequest : BaseRequest
{
    public int Id { get; set; }
}