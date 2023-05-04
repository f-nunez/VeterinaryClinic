using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public interface IClinicService
{
    public Task<DataGridResponse<ClinicDto>> DataGridAsync(GetClinicsRequest request);
    public Task<List<string>> DataGridFilterAddressAsync(string filterValue);
    public Task<List<string>> DataGridFilterEmailAddressAsync(string filterValue);
    public Task<List<string>> DataGridFilterIdAsync(string filterValue);
    public Task<List<string>> DataGridFilterNameAsync(string filterValue);
    public Task<ClinicDto> GetByIdAsync(GetClinicByIdRequest request);
}