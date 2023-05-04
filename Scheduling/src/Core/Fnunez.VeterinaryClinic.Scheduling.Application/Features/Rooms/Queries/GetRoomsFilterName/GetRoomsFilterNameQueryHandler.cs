using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterName;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomsFilterName;

public class GetRoomsFilterNameQueryHandler
    : IRequestHandler<GetRoomsFilterNameQuery, GetRoomsFilterNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomsFilterNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetRoomsFilterNameResponse> Handle(
        GetRoomsFilterNameQuery query,
        CancellationToken cancellationToken)
    {
        GetRoomsFilterNameRequest request = query.GetRoomsFilterNameRequest;
        var response = new GetRoomsFilterNameResponse(request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var roomNames = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.RoomNames = roomNames;
        return response;
    }

    private static string GetSqlQueryToSearch(
        GetRoomsFilterNameRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = LOWER(N'{request.NameFilterValue}');

        SELECT
            DISTINCT TOP(@top) [c].[Name]
        FROM
            [Rooms] AS [c]
        WHERE
            [c].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, LOWER(LTRIM(RTRIM([c].[Name])))) > 0
            )
        ORDER BY [c].[Name];";
    }
}