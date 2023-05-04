using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientsFilterSalutation;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterSalutation;

public class GetClientsFilterSalutationQueryHandler
    : IRequestHandler<GetClientsFilterSalutationQuery, GetClientsFilterSalutationResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsFilterSalutationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientsFilterSalutationResponse> Handle(
        GetClientsFilterSalutationQuery query,
        CancellationToken cancellationToken)
    {
        GetClientsFilterSalutationRequest request = query
            .GetClientsFilterSalutationRequest;

        var response = new GetClientsFilterSalutationResponse(
            request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var clientSalutations = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.ClientSalutations = clientSalutations;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetClientsFilterSalutationRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = LOWER(N'{request.SalutationFilterValue}');

        SELECT
            DISTINCT TOP(@top) [c].[Salutation]
        FROM
            [Clients] AS [c]
        WHERE
            [c].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, LOWER(LTRIM(RTRIM([c].[Salutation])))) > 0
            )
        ORDER BY [c].[Salutation];";
    }
}
