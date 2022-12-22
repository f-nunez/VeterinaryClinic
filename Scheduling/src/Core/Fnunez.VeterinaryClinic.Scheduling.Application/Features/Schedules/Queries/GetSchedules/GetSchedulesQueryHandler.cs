using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.GetSchedules;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Queries.GetSchedules;

public class GetSchedulesQueryHandler
    : IRequestHandler<GetSchedulesQuery, GetSchedulesResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSchedulesQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetSchedulesResponse> Handle(
        GetSchedulesQuery query,
        CancellationToken cancellationToken)
    {
        GetSchedulesRequest request = query.GetSchedulesRequest;
        var response = new GetSchedulesResponse(request.CorrelationId);

        var schedules = await _unitOfWork
            .ReadRepository<Schedule>()
            .ListAsync(cancellationToken);

        if (schedules is null)
            return response;

        response.Schedules = _mapper.Map<List<ScheduleDto>>(schedules);

        return response;
    }
}