using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientsFilterPreferredDoctor;

public class GetPatientsFilterPreferredDoctorResponse : BaseResponse
{
    public DataGridResponse<PreferredDoctorFilterValueDto>? DataGridResponse { get; set; }

    public GetPatientsFilterPreferredDoctorResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}