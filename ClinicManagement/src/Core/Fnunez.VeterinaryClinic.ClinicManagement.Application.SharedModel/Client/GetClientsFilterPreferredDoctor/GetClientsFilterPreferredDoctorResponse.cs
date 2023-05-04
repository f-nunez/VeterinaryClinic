using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredDoctor;

public class GetClientsFilterPreferredDoctorResponse : BaseResponse
{
    public DataGridResponse<PreferredDoctorFilterValueDto>? DataGridResponse { get; set; }

    public GetClientsFilterPreferredDoctorResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}