using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.CreateDoctor;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.UpdateDoctor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface IDoctorService
{
    public Task<DoctorDto> CreateAsync(CreateDoctorRequest createDoctorRequest);
    public Task<DataGridResponse<DoctorDto>> DataGridAsync(GetDoctorsRequest request);
    public Task<List<string>> DataGridFilterFullNameAsync(string filterValue);
    public Task<List<string>> DataGridFilterIdAsync(string filterValue);
    public Task DeleteAsync(int doctorId);
    public Task<DoctorDto> EditAsync(UpdateDoctorRequest updateDoctorRequest);
    public Task<DoctorDto> GetByIdAsync(int doctorId);
}