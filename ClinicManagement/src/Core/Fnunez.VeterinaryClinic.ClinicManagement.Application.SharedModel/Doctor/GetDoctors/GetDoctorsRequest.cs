using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctors;

public class GetDoctorsRequest : BaseRequest
{
    public DataGridRequest DataGridRequest { get; set; } = new();
    public string FullNameFilterValue { get; set; } = null!;
    public string IdFilterValue { get; set; } = null!;
}