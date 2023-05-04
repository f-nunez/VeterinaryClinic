using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterEmailAddress;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterEmailAddress;

public class GetClinicsFilterEmailAddressQueryHandler
    : IRequestHandler<GetClinicsFilterEmailAddressQuery, GetClinicsFilterEmailAddressResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClinicsFilterEmailAddressQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClinicsFilterEmailAddressResponse> Handle(
        GetClinicsFilterEmailAddressQuery query,
        CancellationToken cancellationToken)
    {
        GetClinicsFilterEmailAddressRequest request = query
            .GetClinicsFilterEmailAddressRequest;

        var response = new GetClinicsFilterEmailAddressResponse(
            request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var clinicEmailAddresses = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.ClinicEmailAddresses = clinicEmailAddresses;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetClinicsFilterEmailAddressRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = LOWER(N'{request.EmailAddressFilterValue}');

        SELECT
            DISTINCT TOP(@top) [c].[EmailAddress]
        FROM
            [Clinics] AS [c]
        WHERE
            [c].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, LOWER(LTRIM(RTRIM([c].[EmailAddress])))) > 0
            )
        ORDER BY [c].[EmailAddress];";
    }
}