using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientEdit;

public class GetClientEditRequest : BaseRequest
{
    public int ClientId { get; set; }
}