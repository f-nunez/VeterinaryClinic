using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicById;

public class GetClinicByIdResponse : BaseResponse
{
    public ClinicDto Clinic { get; set; } = new();

    public GetClinicByIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}