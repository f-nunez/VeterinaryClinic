using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Patient.GetPatientDetail;

public class GetPatientDetailResponse : BaseResponse
{
    public PatientDetailDto? PatientDetail { get; set; }

    public GetPatientDetailResponse(Guid correlationId) : base(correlationId)
    {
    }
}