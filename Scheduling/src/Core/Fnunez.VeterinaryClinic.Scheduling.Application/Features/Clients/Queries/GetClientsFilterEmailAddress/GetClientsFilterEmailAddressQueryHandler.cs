using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClientsFilterEmailAddress;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClientsFilterEmailAddress;

public class GetClientsFilterEmailAddressQueryHandler
    : IRequestHandler<GetClientsFilterEmailAddressQuery, GetClientsFilterEmailAddressResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClientsFilterEmailAddressQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClientsFilterEmailAddressResponse> Handle(
        GetClientsFilterEmailAddressQuery query,
        CancellationToken cancellationToken)
    {
        GetClientsFilterEmailAddressRequest request = query
            .GetClientsFilterEmailAddressRequest;

        var response = new GetClientsFilterEmailAddressResponse(
            request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var clientEmailAddresses = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.ClientEmailAddresses = clientEmailAddresses;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetClientsFilterEmailAddressRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = LOWER(N'{request.EmailAddressFilterValue}');

        SELECT
            DISTINCT TOP(@top) [c].[EmailAddress]
        FROM
            [Clients] AS [c]
        WHERE
            [c].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, LOWER(LTRIM(RTRIM([c].[EmailAddress])))) > 0
            )
        ORDER BY [c].[EmailAddress];";
    }
}