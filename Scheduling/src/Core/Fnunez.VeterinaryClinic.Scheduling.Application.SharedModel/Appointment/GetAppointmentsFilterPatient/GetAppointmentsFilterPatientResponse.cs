using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterPatient;

public class GetAppointmentsFilterPatientResponse : BaseResponse
{
    public List<PatientFilterValueDto> PatientFilterValues { get; set; } = new();

    public GetAppointmentsFilterPatientResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}