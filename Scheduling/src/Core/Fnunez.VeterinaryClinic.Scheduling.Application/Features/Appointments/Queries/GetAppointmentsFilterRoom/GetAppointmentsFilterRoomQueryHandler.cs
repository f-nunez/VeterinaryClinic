using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using MediatR;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterRoom;

public class GetAppointmentsFilterRoomQueryHandler
    : IRequestHandler<GetAppointmentsFilterRoomQuery, GetAppointmentsFilterRoomResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAppointmentsFilterRoomQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAppointmentsFilterRoomResponse> Handle(
        GetAppointmentsFilterRoomQuery query,
        CancellationToken cancellationToken)
    {
        GetAppointmentsFilterRoomRequest request = query
            .GetAppointmentsFilterRoomRequest;
        
        var response = new GetAppointmentsFilterRoomResponse(
            request.CorrelationId);
        
        var specification = new RoomFilterValuesSpecification(request);

        var roomFilterValues = await _unitOfWork
            .ReadRepository<Room>()
            .ListAsync(specification, cancellationToken);
        
        var count = await _unitOfWork
            .ReadRepository<Room>()
            .CountAsync(specification, cancellationToken);
        
        if (roomFilterValues is null)
            return response;

        response.DataGridResponse = new DataGridResponse<RoomFilterValueDto>(
            roomFilterValues,
            count
        );
        
        return response;
    }
}
