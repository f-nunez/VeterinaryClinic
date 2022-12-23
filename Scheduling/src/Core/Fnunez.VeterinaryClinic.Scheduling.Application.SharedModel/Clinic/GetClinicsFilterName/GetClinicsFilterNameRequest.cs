using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterName;

public class GetClinicsFilterNameRequest : BaseRequest
{
    public string NameFilterValue { get; set; } = null!;
}