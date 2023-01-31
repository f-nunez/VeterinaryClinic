using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterRoom;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterRoom;

public class RoomFilterValuesSpecification
    : BaseSpecification<Room, RoomFilterValueDto>
{
    public RoomFilterValuesSpecification(
        GetAppointmentsFilterRoomRequest request)
    {
        Query
            .AsNoTracking()
            .Where(r => r.IsActive);

        ApplyFilterSearch(request);

        ApplyOrder(request);

        ApplySkipAndTake(request);

        Query.Select(r =>
            new RoomFilterValueDto
            {
                Id = r.Id,
                Name = r.Name
            }
        );
    }

    private void ApplyFilterSearch(GetAppointmentsFilterRoomRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (string.IsNullOrEmpty(dataGridRequest.Search))
            return;

        string searchFilterValue = dataGridRequest.Search.Trim().ToLower();

        Query.Search(r => r.Name, $"%{searchFilterValue}%");
    }

    private void ApplyOrder(GetAppointmentsFilterRoomRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (dataGridRequest.Sorts is null || !dataGridRequest.Sorts.Any())
            return;

        var orderedBy = SetOrderBy(dataGridRequest);
        SetThenBy(dataGridRequest, orderedBy);
    }

    private void ApplySkipAndTake(GetAppointmentsFilterRoomRequest request)
    {
        Query
            .Skip(request.DataGridRequest.Skip)
            .Take(request.DataGridRequest.Take);
    }

    private IOrderedSpecificationBuilder<Room> SetOrderBy(
        DataGridRequest dataGridRequest)
    {
        var sort = dataGridRequest.Sorts!.FirstOrDefault()!;

        switch (sort.PropertyName)
        {
            case "Name":
                if (sort.IsAscending)
                    return Query.OrderBy(r => r.Name);
                else
                    return Query.OrderByDescending(r => r.Name);

            default:
                throw new SpecificationOrderByException(sort.PropertyName);
        }
    }

    private void SetThenBy(
        DataGridRequest dataGridRequest,
        IOrderedSpecificationBuilder<Room> orderedBy)
    {
        if (dataGridRequest.Sorts!.Count <= 1)
            return;

        for (int i = 1; i < dataGridRequest.Sorts.Count; i++)
        {
            var sort = dataGridRequest.Sorts[i];
            switch (sort.PropertyName)
            {
                case "Name":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(r => r.Name);
                    else
                        orderedBy.ThenByDescending(r => r.Name);
                    break;

                default:
                    throw new SpecificationThenByException(sort.PropertyName);
            }
        }
    }
}