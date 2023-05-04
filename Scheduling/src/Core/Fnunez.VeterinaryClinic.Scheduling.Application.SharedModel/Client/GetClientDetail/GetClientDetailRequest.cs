using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientDetail;

public class GetClientDetailRequest : BaseRequest
{
    public int ClientId { get; set; }
}