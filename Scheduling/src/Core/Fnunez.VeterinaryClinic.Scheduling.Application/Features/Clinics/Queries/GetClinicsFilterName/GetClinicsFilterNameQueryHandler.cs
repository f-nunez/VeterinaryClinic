using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Clinic.GetClinicsFilterName;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clinics.Queries.GetClinicsFilterName;

public class GetClinicsFilterNameQueryHandler
    : IRequestHandler<GetClinicsFilterNameQuery, GetClinicsFilterNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClinicsFilterNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClinicsFilterNameResponse> Handle(
        GetClinicsFilterNameQuery query,
        CancellationToken cancellationToken)
    {
        GetClinicsFilterNameRequest request = query
            .GetClinicsFilterNameRequest;

        var response = new GetClinicsFilterNameResponse(request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var clinicNames = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.ClinicNames = clinicNames;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetClinicsFilterNameRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = LOWER(N'{request.NameFilterValue}');

        SELECT
            DISTINCT TOP(@top) [c].[Name]
        FROM
            [Clinics] AS [c]
        WHERE
            [c].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, LOWER(LTRIM(RTRIM([c].[Name])))) > 0
            )
        ORDER BY [c].[Name];";
    }
}