using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterAppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;

public class GetAppointmentDetailResponse : BaseResponse
{
    public AppointmentDto? Appointment { get; set; }
    public List<AppointmentTypeFilterValueDto> AppointmentTypeFilterValues { get; set; } = new();
    public List<DoctorFilterValueDto> DoctorFilterValues { get; set; } = new();
    public List<RoomFilterValueDto> RoomFilterValues { get; set; } = new();

    public GetAppointmentDetailResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}