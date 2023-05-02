using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterDuration;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterDuration;

public class GetAppointmentTypesFilterDurationQueryHandler
    : IRequestHandler<GetAppointmentTypesFilterDurationQuery, GetAppointmentTypesFilterDurationResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentTypesFilterDurationQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentTypesFilterDurationResponse> Handle(
        GetAppointmentTypesFilterDurationQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentTypesFilterDurationRequest request = query
            .GetAppointmentTypesFilterDurationRequest;

        var response = new GetAppointmentTypesFilterDurationResponse(
            request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var appointmentTypeDurations = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.AppointmentTypeDurations = appointmentTypeDurations;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetAppointmentTypesFilterDurationRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = LOWER(N'{request.DurationFilterValue}');

        SELECT
            DISTINCT TOP(@top) [at].[Duration]
        FROM
            [AppointmentTypes] AS [at]
        WHERE
            [at].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, LOWER(LTRIM(RTRIM([at].[Duration])))) > 0
            )
        ORDER BY [at].[Duration];";
    }
}