using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterFullName;

public class GetClientsFilterFullNameRequest : BaseRequest
{
    public string FullNameFilterValue { get; set; } = null!;
}