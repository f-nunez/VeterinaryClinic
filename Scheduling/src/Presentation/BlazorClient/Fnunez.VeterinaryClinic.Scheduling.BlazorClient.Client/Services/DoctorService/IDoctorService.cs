using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctors;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public interface IDoctorService
{
    public Task<DataGridResponse<DoctorDto>> DataGridAsync(GetDoctorsRequest request);
    public Task<List<string>> DataGridFilterFullNameAsync(string filterValue);
    public Task<List<string>> DataGridFilterIdAsync(string filterValue);
}