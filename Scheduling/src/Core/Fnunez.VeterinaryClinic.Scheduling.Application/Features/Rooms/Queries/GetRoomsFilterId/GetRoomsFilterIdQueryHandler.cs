using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterId;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomsFilterId;

public class GetRoomsFilterIdQueryHandler
    : IRequestHandler<GetRoomsFilterIdQuery, GetRoomsFilterIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomsFilterIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetRoomsFilterIdResponse> Handle(
        GetRoomsFilterIdQuery query,
        CancellationToken cancellationToken)
    {
        GetRoomsFilterIdRequest request = query.GetRoomsFilterIdRequest;
        var response = new GetRoomsFilterIdResponse(request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var roomIds = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.RoomIds = roomIds;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetRoomsFilterIdRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = N'{request.IdFilterValue}';

        SELECT
            TOP(@top) [r].[Id]
        FROM
            [Rooms] AS [r]
        WHERE
            [r].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, CONVERT(VARCHAR(11), [r].[Id])) > 0
            )
        ORDER BY
            [r].[Id];";
    }
}