using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterCode;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterCode;

public class GetAppointmentTypesFilterCodeQueryHandler
    : IRequestHandler<GetAppointmentTypesFilterCodeQuery, GetAppointmentTypesFilterCodeResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentTypesFilterCodeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentTypesFilterCodeResponse> Handle(
        GetAppointmentTypesFilterCodeQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentTypesFilterCodeRequest request = query
            .GetAppointmentTypesFilterCodeRequest;

        var response = new GetAppointmentTypesFilterCodeResponse(
            request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var appointmentTypeCodes = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.AppointemntTypeCodes = appointmentTypeCodes;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetAppointmentTypesFilterCodeRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = LOWER(N'{request.CodeFilterValue}');

        SELECT
            DISTINCT TOP(@top) [at].[Code]
        FROM
            [AppointmentTypes] AS [at]
        WHERE
            [at].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, LOWER(LTRIM(RTRIM([at].[Code])))) > 0
            )
        ORDER BY [at].[Code];";
    }
}