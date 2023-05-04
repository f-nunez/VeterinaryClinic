using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterId;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterId;

public class GetClientsFilterIdQueryHandler
    : IRequestHandler<GetClientsFilterIdQuery, GetClientsFilterIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsFilterIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientsFilterIdResponse> Handle(
        GetClientsFilterIdQuery query,
        CancellationToken cancellationToken)
    {
        GetClientsFilterIdRequest request = query.GetClientsFilterIdRequest;
        var response = new GetClientsFilterIdResponse(request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var clientIds = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.ClientIds = clientIds;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetClientsFilterIdRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = N'{request.IdFilterValue}';

        SELECT
            TOP(@top) [c].[Id]
        FROM
            [Clients] AS [c]
        WHERE
            [c].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, CONVERT(VARCHAR(11), [c].[Id])) > 0
            )
        ORDER BY
            [c].[Id];";
    }
}