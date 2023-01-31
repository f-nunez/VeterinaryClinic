using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRooms;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRooms;

public class RoomsSpecification : BaseSpecification<Room>
{
    public RoomsSpecification(GetRoomsRequest request)
    {
        Query
            .AsNoTracking()
            .Where(r => r.IsActive);

        ApplyFilterId(request);

        ApplyFilterName(request);

        ApplyFilterSearch(request);

        ApplyOrder(request);

        ApplySkipAndTake(request);
    }

    private void ApplyFilterId(GetRoomsRequest request)
    {
        if (string.IsNullOrEmpty(request.IdFilterValue))
            return;

        string idFilterValue = request.IdFilterValue.Trim();

        Query
            .Where(r => r.Id.ToString().Contains(idFilterValue));
    }

    private void ApplyFilterName(GetRoomsRequest request)
    {
        if (string.IsNullOrEmpty(request.NameFilterValue))
            return;

        string nameFilterValue = request.NameFilterValue.Trim().ToLower();

        Query
            .Where(r => r.Name.Trim().ToLower().Contains(nameFilterValue));
    }

    private void ApplyFilterSearch(GetRoomsRequest request)
    {
        if (string.IsNullOrEmpty(request.SearchFilterValue))
            return;

        string searchFilterValue = request.SearchFilterValue.Trim().ToLower();

        Query
            .Search(r => r.Id.ToString(), $"%{searchFilterValue}%")
            .Search(r => r.Name, $"%{searchFilterValue}%");
    }

    private void ApplyOrder(GetRoomsRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (dataGridRequest.Sorts is null || !dataGridRequest.Sorts.Any())
            return;

        var orderedBy = SetOrderBy(dataGridRequest);
        SetThenBy(dataGridRequest, orderedBy);
    }

    private void ApplySkipAndTake(GetRoomsRequest request)
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
            case "Id":
                if (sort.IsAscending)
                    return Query.OrderBy(r => r.Id);
                else
                    return Query.OrderByDescending(r => r.Id);

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
                case "Id":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(r => r.Id);
                    else
                        orderedBy.ThenByDescending(r => r.Id);
                    break;

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