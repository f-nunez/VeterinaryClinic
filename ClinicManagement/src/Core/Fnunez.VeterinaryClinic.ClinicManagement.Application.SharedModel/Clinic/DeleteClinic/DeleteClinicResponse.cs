using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.DeleteClinic;

public class DeleteClinicResponse : BaseResponse
{
    public DeleteClinicResponse(Guid correlationId) : base(correlationId)
    {
    }
}