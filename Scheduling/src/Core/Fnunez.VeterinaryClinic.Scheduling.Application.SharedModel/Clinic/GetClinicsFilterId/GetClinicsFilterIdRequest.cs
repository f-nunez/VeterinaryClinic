using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterId;

public class GetClinicsFilterIdRequest : BaseRequest
{
    public string IdFilterValue { get; set; } = null!;
}