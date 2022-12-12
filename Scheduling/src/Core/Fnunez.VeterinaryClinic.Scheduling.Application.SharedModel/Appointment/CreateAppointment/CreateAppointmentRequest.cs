using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;

public class CreateAppointmentRequest : BaseRequest
{
    public int AppointmentTypeId { get; set; }
    public int ClientId { get; set; }
    public int DoctorId { get; set; }
    public DateTimeOffset DateOfAppointment { get; set; }
    public int PatientId { get; set; }
    public int RoomId { get; set; }
    public Guid ScheduleId { get; set; }
    public string Title { get; set; } = string.Empty;
}