using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicById;

public class GetClinicByIdRequest : BaseRequest
{
    public int Id { get; set; }
}