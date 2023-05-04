using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterPreferredName;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterPreferredName;

public class GetClientsFilterPreferredNameQueryHandler
    : IRequestHandler<GetClientsFilterPreferredNameQuery, GetClientsFilterPreferredNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsFilterPreferredNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientsFilterPreferredNameResponse> Handle(
        GetClientsFilterPreferredNameQuery query,
        CancellationToken cancellationToken)
    {
        GetClientsFilterPreferredNameRequest request = query
            .GetClientsFilterPreferredNameRequest;

        var response = new GetClientsFilterPreferredNameResponse(
            request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var clientPreferredNames = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.ClientPreferredNames = clientPreferredNames;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetClientsFilterPreferredNameRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = LOWER(N'{request.PreferredNameFilterValue}');

        SELECT
            DISTINCT TOP(@top) [c].[PreferredName]
        FROM
            [Clients] AS [c]
        WHERE
            [c].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, LOWER(LTRIM(RTRIM([c].[PreferredName])))) > 0
            )
        ORDER BY [c].[PreferredName];";
    }
}