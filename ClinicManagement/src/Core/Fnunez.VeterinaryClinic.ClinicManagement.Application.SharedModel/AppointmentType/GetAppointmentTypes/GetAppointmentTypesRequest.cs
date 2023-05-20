using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypes;

public class GetAppointmentTypesRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
    public string CodeFilterValue { get; set; } = null!;
    public string DurationFilterValue { get; set; } = null!;
    public string IdFilterValue { get; set; } = null!;
    public string NameFilterValue { get; set; } = null!;
}