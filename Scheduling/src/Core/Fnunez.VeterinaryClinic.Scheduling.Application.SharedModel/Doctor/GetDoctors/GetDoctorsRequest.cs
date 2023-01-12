using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctors;

public class GetDoctorsRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
    public string FullNameFilterValue { get; set; } = null!;
    public string IdFilterValue { get; set; } = null!;
    public string SearchFilterValue { get; set; } = null!;
}