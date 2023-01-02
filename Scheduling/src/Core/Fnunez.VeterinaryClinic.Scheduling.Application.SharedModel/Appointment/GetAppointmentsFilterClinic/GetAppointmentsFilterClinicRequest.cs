using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClinic;

public class GetAppointmentsFilterClinicRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
}