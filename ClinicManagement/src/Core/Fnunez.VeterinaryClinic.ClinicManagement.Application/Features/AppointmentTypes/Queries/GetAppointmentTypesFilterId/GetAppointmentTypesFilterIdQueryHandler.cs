using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypesFilterId;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypesFilterId;

public class GetAppointmentTypesFilterIdQueryHandler
    : IRequestHandler<GetAppointmentTypesFilterIdQuery, GetAppointmentTypesFilterIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentTypesFilterIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentTypesFilterIdResponse> Handle(
        GetAppointmentTypesFilterIdQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentTypesFilterIdRequest request = query
            .GetAppointmentTypesFilterIdRequest;

        var response = new GetAppointmentTypesFilterIdResponse(
            request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var appointmentTypeIds = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.AppointmentTypeIds = appointmentTypeIds;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetAppointmentTypesFilterIdRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = N'{request.IdFilterValue}';

        SELECT
            TOP(@top) [at].[Id]
        FROM
            [AppointmentTypes] AS [at]
        WHERE
            [at].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, CONVERT(VARCHAR(11), [at].[Id])) > 0
            )
        ORDER BY
            [at].[Id];";
    }
}
