using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;

public class GetAppointmentsRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
    public string ClientIdFilterValue { get; set; } = string.Empty;
    public string ClinicIdFilterValue { get; set; } = string.Empty;
    public string PatientIdFilterValue { get; set; } = string.Empty;
    public DateTimeOffset StartOn { get; set; }
    public DateTimeOffset EndOn { get; set; }
}