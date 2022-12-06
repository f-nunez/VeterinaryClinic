using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;

public class AppointmentDto
{
    public Guid AppointmentId { get; set; }
    public int AppointmentTypeId { get; set; }
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public bool IsAllDay { get; set; }
    public bool IsConfirmed { get; set; }
    public bool IsPotentiallyConflicting { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int RoomId { get; set; }
    public Guid ScheduleId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset StartOn { get; set; }
    public DateTimeOffset EndOn { get; set; }
    public AppointmentTypeDto AppointmentType { get; set; } = new();

    public AppointmentDto ShallowCopy()
    {
        return (AppointmentDto)this.MemberwiseClone();
    }

    public override string ToString()
    {
        return $"Id: {AppointmentId} \nRoomId: {RoomId}\nDoctorId: {DoctorId}\nClient: {ClientId} {ClientName}\nPatient: {PatientId} {PatientName}\nStart: {StartOn}\nEnd:{EndOn}";
    }
}