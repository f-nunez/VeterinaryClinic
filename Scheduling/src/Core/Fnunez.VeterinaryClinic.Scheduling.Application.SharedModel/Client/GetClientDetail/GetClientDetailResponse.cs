using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientDetail;

public class GetClientDetailResponse : BaseResponse
{
    public ClientDetailDto? ClientDetail { get; set; }

    public GetClientDetailResponse(Guid correlationId) : base(correlationId)
    {
    }
}