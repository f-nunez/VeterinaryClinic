using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterPreferredDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientEdit;

public class GetClientEditResponse : BaseResponse
{
    public ClientDto? Client { get; set; }
    public List<PreferredDoctorFilterValueDto> PreferredDoctorFilterValues { get; set; } = new();

    public GetClientEditResponse(Guid correlationId)
        : base(correlationId)
    {
    }
}