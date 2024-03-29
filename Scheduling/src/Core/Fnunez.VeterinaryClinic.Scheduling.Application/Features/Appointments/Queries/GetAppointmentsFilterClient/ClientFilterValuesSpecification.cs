using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentsFilterClient;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentsFilterClient;

public class ClientFilterValuesSpecification
    : BaseSpecification<Client, ClientFilterValueDto>
{
    public ClientFilterValuesSpecification(
        GetAppointmentsFilterClientRequest request)
    {
        Query
            .AsNoTracking()
            .Where(c => c.IsActive);

        ApplyOrder(request);

        ApplySearch(request);

        ApplySkipAndTake(request);

        Query.Select(c =>
            new ClientFilterValueDto
            {
                EmailAddress = c.EmailAddress,
                FullName = c.FullName,
                Id = c.Id
            }
        );
    }

    private void ApplyOrder(GetAppointmentsFilterClientRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (dataGridRequest.Sorts is null || !dataGridRequest.Sorts.Any())
            return;

        var orderedBy = SetOrderBy(dataGridRequest);
        SetThenBy(dataGridRequest, orderedBy);
    }

    private void ApplySearch(GetAppointmentsFilterClientRequest request)
    {
        if (string.IsNullOrEmpty(request.DataGridRequest.Search))
            return;

        string search = request.DataGridRequest.Search.Trim().ToLower();

        if (string.IsNullOrEmpty(search))
            return;

        Query
            .Search(c => c.EmailAddress, $"%{search}%")
            .Search(c => c.FullName, $"%{search}%");
    }

    private void ApplySkipAndTake(GetAppointmentsFilterClientRequest request)
    {
        Query
            .Skip(request.DataGridRequest.Skip)
            .Take(request.DataGridRequest.Take);
    }

    private IOrderedSpecificationBuilder<Client> SetOrderBy(
        DataGridRequest dataGridRequest)
    {
        var sort = dataGridRequest.Sorts!.FirstOrDefault()!;

        switch (sort.PropertyName)
        {
            case "EmailAddress":
                if (sort.IsAscending)
                    return Query.OrderBy(c => c.EmailAddress);
                else
                    return Query.OrderByDescending(c => c.EmailAddress);

            case "FullName":
                if (sort.IsAscending)
                    return Query.OrderBy(c => c.FullName);
                else
                    return Query.OrderByDescending(c => c.FullName);

            default:
                throw new SpecificationOrderByException(sort.PropertyName);
        }
    }

    private void SetThenBy(
        DataGridRequest dataGridRequest,
        IOrderedSpecificationBuilder<Client> orderedBy)
    {
        if (dataGridRequest.Sorts!.Count <= 1)
            return;

        for (int i = 1; i < dataGridRequest.Sorts.Count; i++)
        {
            var sort = dataGridRequest.Sorts[i];
            switch (sort.PropertyName)
            {
                case "EmailAddress":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(c => c.EmailAddress);
                    else
                        orderedBy.ThenByDescending(c => c.EmailAddress);
                    break;

                case "FullName":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(c => c.FullName);
                    else
                        orderedBy.ThenByDescending(c => c.FullName);
                    break;

                default:
                    throw new SpecificationThenByException(sort.PropertyName);
            }
        }
    }
}