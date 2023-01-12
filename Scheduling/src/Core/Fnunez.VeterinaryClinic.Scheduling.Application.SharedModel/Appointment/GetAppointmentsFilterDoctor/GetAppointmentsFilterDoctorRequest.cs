using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;

public class GetAppointmentsFilterDoctorRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
}