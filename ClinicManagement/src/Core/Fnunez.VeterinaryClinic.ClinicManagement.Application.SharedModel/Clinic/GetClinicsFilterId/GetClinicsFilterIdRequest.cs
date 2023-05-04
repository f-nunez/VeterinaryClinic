using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterId;

public class GetClinicsFilterIdRequest : BaseRequest
{
    public string IdFilterValue { get; set; } = null!;
}