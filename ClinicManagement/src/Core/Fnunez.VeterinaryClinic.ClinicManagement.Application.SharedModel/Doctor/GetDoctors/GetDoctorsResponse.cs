using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;

public class GetDoctorsResponse : BaseResponse
{
    public DataGridResponse<DoctorDto>? DataGridResponse { get; set; }

    public GetDoctorsResponse(Guid correlationId) : base(correlationId)
    {
    }
}