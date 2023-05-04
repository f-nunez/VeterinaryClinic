using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicById;

public class GetClinicByIdResponse : BaseResponse
{
    public ClinicDto Clinic { get; set; } = new();

    public GetClinicByIdResponse(Guid correlationId) : base(correlationId)
    {
    }
}