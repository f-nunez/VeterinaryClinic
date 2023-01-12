using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterDoctor;

public class GetAppointmentsFilterDoctorResponse : BaseResponse
{
    public DataGridResponse<DoctorFilterValueDto>? DataGridResponse { get; set; }

    public GetAppointmentsFilterDoctorResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}