using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientDetail;

public class GetClientDetailRequest : BaseRequest
{
    public int ClientId { get; set; }
}