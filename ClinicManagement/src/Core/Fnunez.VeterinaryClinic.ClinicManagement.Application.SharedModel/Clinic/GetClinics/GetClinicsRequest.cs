using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinics;

public class GetClinicsRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
    public string AddressFilterValue { get; set; } = string.Empty;
    public string EmailAddressFilterValue { get; set; } = string.Empty;
    public string IdFilterValue { get; set; } = string.Empty;
    public string NameFilterValue { get; set; } = string.Empty;
    public string SearchFilterValue { get; set; } = string.Empty;
}