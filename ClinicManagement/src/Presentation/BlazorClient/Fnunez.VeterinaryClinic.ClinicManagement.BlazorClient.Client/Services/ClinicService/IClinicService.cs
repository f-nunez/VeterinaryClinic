using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface IClinicService
{
    public Task<DataGridResponse<ClinicDto>> DataGridAsync(GetClinicsRequest request);
    public Task<List<string>> DataGridFilterAddressAsync(string filterValue);
    public Task<List<string>> DataGridFilterEmailAddressAsync(string filterValue);
    public Task<List<string>> DataGridFilterIdAsync(string filterValue);
    public Task<List<string>> DataGridFilterNameAsync(string filterValue);
}