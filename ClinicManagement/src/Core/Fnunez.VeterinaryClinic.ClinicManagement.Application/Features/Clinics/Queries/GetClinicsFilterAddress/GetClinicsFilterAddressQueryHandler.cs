using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterAddress;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterAddress;

public class GetClinicsFilterAddressQueryHandler
    : IRequestHandler<GetClinicsFilterAddressQuery, GetClinicsFilterAddressResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClinicsFilterAddressQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClinicsFilterAddressResponse> Handle(
        GetClinicsFilterAddressQuery query,
        CancellationToken cancellationToken)
    {
        GetClinicsFilterAddressRequest request = query
            .GetClinicsFilterAddressRequest;

        var response = new GetClinicsFilterAddressResponse(
            request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var clinicAddresses = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.ClinicAddresses = clinicAddresses;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetClinicsFilterAddressRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = LOWER(N'{request.AddressFilterValue}');

        SELECT
            DISTINCT TOP(@top) [c].[Address]
        FROM
            [Clinics] AS [c]
        WHERE
            [c].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, LOWER(LTRIM(RTRIM([c].[Address])))) > 0
            )
        ORDER BY [c].[Address];";
    }
}