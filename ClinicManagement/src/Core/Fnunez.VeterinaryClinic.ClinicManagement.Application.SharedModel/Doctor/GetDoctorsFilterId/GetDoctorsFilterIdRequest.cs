using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterId;

public class GetDoctorsFilterIdRequest : BaseRequest
{
    public string IdFilterValue { get; set; } = null!;
}