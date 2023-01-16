using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.UpdateClinic;

public class UpdateClinicResponse : BaseResponse
{
    public ClinicDto Clinic { get; set; } = new();

    public UpdateClinicResponse(Guid correlationId) : base(correlationId)
    {
    }
}