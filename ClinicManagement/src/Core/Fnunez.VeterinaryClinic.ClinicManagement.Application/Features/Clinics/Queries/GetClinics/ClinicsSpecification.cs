using Fnunez.VeterinaryClinic.ClinicManagement.Application.Common.Exceptions;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Clinic.GetClinics;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications.Builders;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clinics.Queries.GetClinics;

public class ClinicsSpecification : BaseSpecification<Clinic>
{
    public ClinicsSpecification(GetClinicsRequest request)
    {
        Query.AsNoTracking();

        ApplyFilterAddress(request);

        ApplyFilterEmailAddress(request);

        ApplyFilterId(request);

        ApplyFilterName(request);

        ApplyFilterSearch(request);

        ApplyOrder(request);

        ApplySkipAndTake(request);
    }

    private void ApplyFilterAddress(GetClinicsRequest request)
    {
        if (string.IsNullOrEmpty(request.AddressFilterValue))
            return;

        string addressFilterValue = request.AddressFilterValue
            .Trim().ToLower();

        Query
            .Where(c => c.Address.Trim().ToLower().Contains(
                addressFilterValue));
    }

    private void ApplyFilterEmailAddress(GetClinicsRequest request)
    {
        if (string.IsNullOrEmpty(request.EmailAddressFilterValue))
            return;

        string emailAddressFilterValue = request.EmailAddressFilterValue
            .Trim().ToLower();

        Query
            .Where(c => c.EmailAddress.Trim().ToLower().Contains(
                emailAddressFilterValue));
    }

    private void ApplyFilterId(GetClinicsRequest request)
    {
        if (string.IsNullOrEmpty(request.IdFilterValue))
            return;

        string idFilterValue = request.IdFilterValue.Trim();

        Query
            .Where(c => c.Id.ToString().Contains(idFilterValue));
    }

    private void ApplyFilterName(GetClinicsRequest request)
    {
        if (string.IsNullOrEmpty(request.NameFilterValue))
            return;

        string nameFilterValue = request.NameFilterValue.Trim().ToLower();

        Query
            .Where(c => c.Name.Trim().ToLower().Contains(nameFilterValue));
    }

    private void ApplyFilterSearch(GetClinicsRequest request)
    {
        if (string.IsNullOrEmpty(request.SearchFilterValue))
            return;

        string searchFilterValue = request.SearchFilterValue.Trim().ToLower();

        Query
            .Search(c => c.Address, $"%{searchFilterValue}%")
            .Search(c => c.EmailAddress, $"%{searchFilterValue}%")
            .Search(c => c.Id.ToString(), $"%{searchFilterValue}%")
            .Search(c => c.Name, $"%{searchFilterValue}%");
    }

    private void ApplyOrder(GetClinicsRequest request)
    {
        DataGridRequest dataGridRequest = request.DataGridRequest;

        if (dataGridRequest.Sorts is null || !dataGridRequest.Sorts.Any())
            return;

        var orderedBy = SetOrderBy(dataGridRequest);
        SetThenBy(dataGridRequest, orderedBy);
    }

    private void ApplySkipAndTake(GetClinicsRequest request)
    {
        Query
            .Skip(request.DataGridRequest.Skip)
            .Take(request.DataGridRequest.Take);
    }

    private IOrderedSpecificationBuilder<Clinic> SetOrderBy(
        DataGridRequest dataGridRequest)
    {
        var sort = dataGridRequest.Sorts!.FirstOrDefault()!;

        switch (sort.PropertyName)
        {
            case "Address":
                if (sort.IsAscending)
                    return Query.OrderBy(c => c.Address);
                else
                    return Query.OrderByDescending(c => c.Address);

            case "EmailAddress":
                if (sort.IsAscending)
                    return Query.OrderBy(c => c.EmailAddress);
                else
                    return Query.OrderByDescending(c => c.EmailAddress);

            case "Id":
                if (sort.IsAscending)
                    return Query.OrderBy(c => c.Id);
                else
                    return Query.OrderByDescending(c => c.Id);

            case "Name":
                if (sort.IsAscending)
                    return Query.OrderBy(c => c.Name);
                else
                    return Query.OrderByDescending(c => c.Name);

            default:
                throw new SpecificationOrderByException(sort.PropertyName);
        }
    }

    private void SetThenBy(
        DataGridRequest dataGridRequest,
        IOrderedSpecificationBuilder<Clinic> orderedBy)
    {
        if (dataGridRequest.Sorts!.Count <= 1)
            return;

        for (int i = 1; i < dataGridRequest.Sorts.Count; i++)
        {
            var sort = dataGridRequest.Sorts[i];
            switch (sort.PropertyName)
            {
                case "Address":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(c => c.Address);
                    else
                        orderedBy.ThenByDescending(c => c.Address);
                    break;

                case "EmailAddress":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(c => c.EmailAddress);
                    else
                        orderedBy.ThenByDescending(c => c.EmailAddress);
                    break;

                case "Id":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(c => c.Id);
                    else
                        orderedBy.ThenByDescending(c => c.Id);
                    break;

                case "Name":
                    if (sort.IsAscending)
                        orderedBy.ThenBy(c => c.Name);
                    else
                        orderedBy.ThenByDescending(c => c.Name);
                    break;

                default:
                    throw new SpecificationThenByException(sort.PropertyName);
            }
        }
    }
}