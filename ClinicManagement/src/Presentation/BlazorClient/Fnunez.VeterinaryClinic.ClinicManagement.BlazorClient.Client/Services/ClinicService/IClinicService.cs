using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.CreateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.DeleteClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.UpdateClinic;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface IClinicService
{
    public Task<ClinicDto> CreateAsync(CreateClinicRequest request);
    public Task<DataGridResponse<ClinicDto>> DataGridAsync(GetClinicsRequest request);
    public Task<List<string>> DataGridFilterAddressAsync(string filterValue);
    public Task<List<string>> DataGridFilterEmailAddressAsync(string filterValue);
    public Task<List<string>> DataGridFilterIdAsync(string filterValue);
    public Task<List<string>> DataGridFilterNameAsync(string filterValue);
    public Task DeleteAsync(DeleteClinicRequest request);
    public Task<ClinicDto> GetByIdAsync(GetClinicByIdRequest request);
    public Task<ClinicDto> UpdateAsync(UpdateClinicRequest request);
}