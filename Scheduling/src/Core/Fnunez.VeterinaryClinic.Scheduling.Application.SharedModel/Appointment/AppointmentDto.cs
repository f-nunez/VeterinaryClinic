namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment;

public class AppointmentDto
{
    public Guid AppointmentId { get; set; }
    public int AppointmentTypeId { get; set; }
    public int ClientId { get; set; }
    public int ClinicId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int DoctorId { get; set; }
    public bool IsConfirmed { get; set; }
    public int PatientId { get; set; }
    public int RoomId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset StartOn { get; set; }
    public DateTimeOffset EndOn { get; set; }

    public AppointmentDto ShallowCopy()
    {
        return (AppointmentDto)this.MemberwiseClone();
    }

    public override string ToString()
    {
        return $"Id: {AppointmentId} \nRoomId: {RoomId}\nDoctorId: {DoctorId}\nClient: {ClientId}\nPatient: {PatientId}\nStart: {StartOn}\nEnd:{EndOn}";
    }
}