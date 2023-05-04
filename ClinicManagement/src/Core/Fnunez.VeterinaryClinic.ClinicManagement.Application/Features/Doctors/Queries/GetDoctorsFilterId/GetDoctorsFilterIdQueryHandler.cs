using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Doctor.GetDoctorsFilterId;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Doctors.Queries.GetDoctorsFilterId;

public class GetDoctorsFilterIdQueryHandler
    : IRequestHandler<GetDoctorsFilterIdQuery, GetDoctorsFilterIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDoctorsFilterIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDoctorsFilterIdResponse> Handle(
        GetDoctorsFilterIdQuery query,
        CancellationToken cancellationToken)
    {
        GetDoctorsFilterIdRequest request = query.GetDoctorsFilterIdRequest;
        var response = new GetDoctorsFilterIdResponse(request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var doctorIds = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.DoctorIds = doctorIds;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetDoctorsFilterIdRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = N'{request.IdFilterValue}';

        SELECT
            TOP(@top) [d].[Id]
        FROM
            [Doctors] AS [d]
        WHERE
            [d].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, CONVERT(VARCHAR(11), [d].[Id])) > 0
            )
        ORDER BY
            [d].[Id];";
    }
}