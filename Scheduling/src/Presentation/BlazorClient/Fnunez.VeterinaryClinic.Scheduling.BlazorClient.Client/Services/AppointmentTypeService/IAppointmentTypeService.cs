using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypes;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public interface IAppointmentTypeService
{
    public Task<DataGridResponse<AppointmentTypeDto>> DataGridAsync(GetAppointmentTypesRequest request);
    public Task<List<string>> DataGridFilterCodeAsync(string filterValue);
    public Task<List<string>> DataGridFilterDurationAsync(string filterValue);
    public Task<List<string>> DataGridFilterIdAsync(string filterValue);
    public Task<List<string>> DataGridFilterNameAsync(string filterValue);
}