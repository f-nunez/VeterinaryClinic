using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterSalutation;

public class GetClientsFilterSalutationRequest : BaseRequest
{
    public string SalutationFilterValue { get; set; } = null!;
}