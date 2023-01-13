using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterFullName;

public class GetDoctorsFilterFullNameRequest : BaseRequest
{
    public string FullNameFilterValue { get; set; } = null!;
}