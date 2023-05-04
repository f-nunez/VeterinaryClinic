using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterFullName;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterFullName;

public class GetClientsFilterFullNameQueryHandler
    : IRequestHandler<GetClientsFilterFullNameQuery, GetClientsFilterFullNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsFilterFullNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientsFilterFullNameResponse> Handle(
        GetClientsFilterFullNameQuery query,
        CancellationToken cancellationToken)
    {
        GetClientsFilterFullNameRequest request = query
            .GetClientsFilterFullNameRequest;

        var response = new GetClientsFilterFullNameResponse(
            request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var clientFullNames = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.ClientFullNames = clientFullNames;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetClientsFilterFullNameRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = LOWER(N'{request.FullNameFilterValue}');

        SELECT
            DISTINCT TOP(@top) [c].[FullName]
        FROM
            [Clients] AS [c]
        WHERE
            [c].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, LOWER(LTRIM(RTRIM([c].[FullName])))) > 0
            )
        ORDER BY [c].[FullName];";
    }
}