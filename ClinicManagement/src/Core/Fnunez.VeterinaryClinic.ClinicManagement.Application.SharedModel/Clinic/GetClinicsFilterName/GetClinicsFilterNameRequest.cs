using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterName;

public class GetClinicsFilterNameRequest : BaseRequest
{
    public string NameFilterValue { get; set; } = null!;
}