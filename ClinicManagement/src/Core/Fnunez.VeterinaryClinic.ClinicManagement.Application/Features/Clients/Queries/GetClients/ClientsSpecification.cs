using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClients;

public class ClientsSpecification : BaseSpecification<Client>
{
    public ClientsSpecification(GetClientsRequest request)
    {
        Query
            .AsNoTracking()
            .Where(c => c.IsActive);

        ApplyFilterEmailAddress(request);

        ApplyFilterFullName(request);

        ApplyFilterId(request);

        ApplyFilterPreferredName(request);

        ApplyFilterSalutation(request);

        ApplyOrder(request);

        ApplySearch(request);

        ApplySkipAndTake(request);
    }

    private void ApplyFilterEmailAddress(GetClientsRequest request)
    {
        if (string.IsNullOrEmpty(request.EmailAddressFilterValue))
            return;

        string emailAddressFilterValue = request.EmailAddressFilterValue
            .Trim().ToLower();

        Query
            .Where(c => c.EmailAddress.Trim().ToLower().Contains(
                emailAddressFilterValue));
    }

    private void ApplyFilterFullName(GetClientsRequest request)
    {
        if (string.IsNullOrEmpty(request.FullNameFilterValue))
            return;

        string nameFilterValue = request.FullNameFilterValue.Trim().ToLower();

        Query
            .Where(c => c.FullName.Trim().ToLower().Contains(nameFilterValue));
    }

    private void ApplyFilterId(GetClientsRequest request)
    {
        if (string.IsNullOrEmpty(request.IdFilterValue))
            return;

        string idFilterValue = request.IdFilterValue.Trim();

        Query
            .Where(c => c.Id.ToString().Contains(idFilterValue));
    }

    private void ApplyFilterPreferredName(GetClientsRequest request)
    {
        if (string.IsNullOrEmpty(request.PreferredNameFilterValue))
            return;

        string preferredNameFilterValue = request.PreferredNameFilterValue
            .Trim().ToLower();

        Query
            .Where(c => c.PreferredName.Trim().ToLower().Contains(
                preferredNameFilterValue));
    }

    private void ApplyFilterSalutation(GetClientsRequest request)
    {
        if (string.IsNullOrEmpty(request.SalutationFilterValue))
            return;

        string salutationFilterValue = request.SalutationFilterValue
            .Trim().ToLower();

        Query
            .Where(c => c.Salutation.Trim().ToLower().Contains(
                salutationFilterValue));
    }

    private void ApplyOrder(GetClientsRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (dataGridRequest.Sorts is null || !dataGridRequest.Sorts.Any())
            return;

        var orderedBy = SetOrderBy(dataGridRequest);
        SetThenBy(dataGridRequest, orderedBy);
    }

    private void ApplySearch(GetClientsRequest request)
    {
        if (string.IsNullOrEmpty(request.DataGridRequest.Search))
            return;

        string search = request.DataGridRequest.Search.Trim().ToLower();

        if (string.IsNullOrEmpty(request.DataGridRequest.Search))
            return;

        Query
            .Search(c => c.EmailAddress, $"%{search}%")
            .Search(c => c.FullName, $"%{search}%")
            .Search(c => c.Id.ToString(), $"%{search}%")
            .Search(c => c.PreferredName, $"%{search}%")
            .Search(c => c.Salutation, $"%{search}%");
    }

    private void ApplySkipAndTake(GetClientsRequest request)
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
            case "ClientId":
                if (sort.IsAscending)
                    return Query.OrderBy(c => c.Id);
                else
                    return Query.OrderByDescending(c => c.Id);

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

            case "PreferredName":
                if (sort.IsAscending)
                    return Query.OrderBy(c => c.PreferredName);
                else
                    return Query.OrderByDescending(c => c.PreferredName);

            case "Salutation":
                if (sort.IsAscending)
                    return Query.OrderBy(c => c.Salutation);
                else
                    return Query.OrderByDescending(c => c.Salutation);

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
                case "ClientId":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(c => c.Id);
                    else
                        orderedBy.ThenByDescending(c => c.Id);
                    break;

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

                case "PreferredName":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(c => c.PreferredName);
                    else
                        orderedBy.ThenByDescending(c => c.PreferredName);
                    break;

                case "Salutation":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(c => c.Salutation);
                    else
                        orderedBy.ThenByDescending(c => c.Salutation);
                    break;

                default:
                    throw new SpecificationThenByException(sort.PropertyName);
            }
        }
    }
}