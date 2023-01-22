using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.CreateClinic;

public class CreateClinicResponse : BaseResponse
{
    public ClinicDto Clinic { get; set; } = new();

    public CreateClinicResponse(Guid correlationId) : base(correlationId)
    {
    }
}