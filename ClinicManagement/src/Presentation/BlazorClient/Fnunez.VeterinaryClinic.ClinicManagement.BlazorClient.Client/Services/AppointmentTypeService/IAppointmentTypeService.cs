using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.CreateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.UpdateAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface IAppointmentTypeService
{
    public Task<AppointmentTypeDto> CreateAsync(CreateAppointmentTypeRequest createAppointmentTypeRequest);
    public Task<DataGridResponse<AppointmentTypeDto>> DataGridAsync(GetAppointmentTypesRequest request);
    public Task<List<string>> DataGridFilterCodeAsync(string filterValue);
    public Task<List<string>> DataGridFilterDurationAsync(string filterValue);
    public Task<List<string>> DataGridFilterIdAsync(string filterValue);
    public Task<List<string>> DataGridFilterNameAsync(string filterValue);
    public Task DeleteAsync(int appointmentTypeId);
    public Task<AppointmentTypeDto> EditAsync(UpdateAppointmentTypeRequest updateAppointmentTypeRequest);
    public Task<AppointmentTypeDto> GetByIdAsync(int appointmentTypeId);
}