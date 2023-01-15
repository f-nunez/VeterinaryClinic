using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientDetail;

public class GetClientDetailResponse : BaseResponse
{
    public ClientDetailDto? ClientDetail { get; set; }

    public GetClientDetailResponse(Guid correlationId) : base(correlationId)
    {
    }
}