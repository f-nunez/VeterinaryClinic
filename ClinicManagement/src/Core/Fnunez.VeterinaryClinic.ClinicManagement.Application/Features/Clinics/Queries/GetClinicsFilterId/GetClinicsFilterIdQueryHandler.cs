using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinicsFilterId;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinicsFilterId;

public class GetClinicsFilterIdQueryHandler
    : IRequestHandler<GetClinicsFilterIdQuery, GetClinicsFilterIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetClinicsFilterIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetClinicsFilterIdResponse> Handle(
        GetClinicsFilterIdQuery query,
        CancellationToken cancellationToken)
    {
        GetClinicsFilterIdRequest request = query
            .GetClinicsFilterIdRequest;

        var response = new GetClinicsFilterIdResponse(request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var clinicIds = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.ClinicIds = clinicIds;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetClinicsFilterIdRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = N'{request.IdFilterValue}';

        SELECT
            TOP(@top) [c].[Id]
        FROM
            [Clinics] AS [c]
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