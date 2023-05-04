using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorsFilterFullName;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorsFilterFullName;

public class GetDoctorsFilterFullNameQueryHandler
    : IRequestHandler<GetDoctorsFilterFullNameQuery, GetDoctorsFilterFullNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDoctorsFilterFullNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDoctorsFilterFullNameResponse> Handle(
        GetDoctorsFilterFullNameQuery query,
        CancellationToken cancellationToken)
    {
        GetDoctorsFilterFullNameRequest request = query
            .GetDoctorsFilterFullNameRequest;

        var response = new GetDoctorsFilterFullNameResponse(
            request.CorrelationId);

        string sqlQueryToSearch = GetSqlQueryToSearch(request);

        var doctorFullNames = await _unitOfWork
            .GetFromRawSqlAsync<string>(sqlQueryToSearch, cancellationToken);

        response.DoctorFullNames = doctorFullNames;

        return response;
    }

    private static string GetSqlQueryToSearch(
        GetDoctorsFilterFullNameRequest request)
    {
        return @$"
        DECLARE @top AS INTEGER = 10;
        DECLARE @search as NVARCHAR(4000) = LOWER(N'{request.FullNameFilterValue}');

        SELECT
            DISTINCT TOP(@top) [d].[FullName]
        FROM
            [Doctors] AS [d]
        WHERE
            [d].[IsActive] = CAST(1 AS BIT) AND
            (
                (@search LIKE N'') OR
                CHARINDEX(@search, LOWER(LTRIM(RTRIM([d].[FullName])))) > 0
            )
        ORDER BY [d].[FullName];";
    }
}