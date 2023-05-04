using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentEdit;

public class GetAppointmentEditResponse : BaseResponse
{
    public AppointmentEditDto? Appointment { get; set; }
    public List<AppointmentTypeFilterValueDto> AppointmentTypeFilterValues { get; set; } = new();
    public List<DoctorFilterValueDto> DoctorFilterValues { get; set; } = new();
    public List<RoomFilterValueDto> RoomFilterValues { get; set; } = new();

    public GetAppointmentEditResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}