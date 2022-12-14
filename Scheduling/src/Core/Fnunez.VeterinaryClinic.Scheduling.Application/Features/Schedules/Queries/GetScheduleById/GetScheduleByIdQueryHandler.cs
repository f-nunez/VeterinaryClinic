using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Schedule.GetScheduleById;
using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Schedules.Queries.GetScheduleById;

public class GetScheduleByIdQueryHandler : IRequestHandler<GetScheduleByIdQuery, GetScheduleByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetScheduleByIdQueryHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetScheduleByIdResponse> Handle(
        GetScheduleByIdQuery query,
        CancellationToken cancellationToken)
    {
        GetScheduleByIdRequest request = query.GetScheduleByIdRequest;
        var response = new GetScheduleByIdResponse(request.CorrelationId);

        var schedule = await _unitOfWork.ReadRepository<Schedule>()
            .GetByIdAsync(request.Id);

        if (schedule is null)
            throw new NotFoundException(nameof(schedule), request.Id);

        response.Schedule = _mapper.Map<ScheduleDto>(schedule);

        return response;
    }
}
