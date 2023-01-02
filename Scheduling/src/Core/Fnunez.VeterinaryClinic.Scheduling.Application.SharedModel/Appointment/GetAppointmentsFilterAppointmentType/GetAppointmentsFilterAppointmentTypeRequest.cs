using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;

public class GetAppointmentsFilterAppointmentTypeRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
}