using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterName;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterName;

public class GetAppointmentTypesFilterNameQueryHandler
    : IRequestHandler<GetAppointmentTypesFilterNameQuery, GetAppointmentTypesFilterNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentTypesFilterNameQueryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentTypesFilterNameResponse> Handle(
        GetAppointmentTypesFilterNameQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentTypesFilterNameRequest request = query
            .GetAppointmentTypesFilterNameRequest;

        var response = new GetAppointmentTypesFilterNameResponse(
            request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var appointmentTypeNames = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.AppointemntTypeNames = appointmentTypeNames;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetAppointmentTypesFilterNameRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = LOWER(N'{request.NameFilterValue}');

        SELECT
            DISTINCT TOP(@top) [at].[Name]
        FROM
            [AppointmentTypes] AS [at]
        WHERE
            [at].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, LOWER(LTRIM(RTRIM([at].[Name])))) > 0
            )
        ORDER BY [at].[Name];";
    }
}