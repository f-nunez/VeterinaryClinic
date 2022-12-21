using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Client.GetClients;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClients;

public class ClientsCountSpecification : BaseSpecification<Client>
{
    public ClientsCountSpecification(GetClientsRequest request)
    {
        Query.AsNoTracking();

        ApplyFilterEmailAddress(request);

        ApplyFilterFullName(request);

        ApplyFilterId(request);

        ApplyFilterPreferredName(request);

        ApplyFilterSalutation(request);

        ApplyFilterSearch(request);
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

    private void ApplyFilterSearch(GetClientsRequest request)
    {
        if (string.IsNullOrEmpty(request.SearchFilterValue))
            return;

        string searchFilterValue = request.SearchFilterValue.Trim().ToLower();

        Query
            .Search(c => c.EmailAddress, $"%{searchFilterValue}%")
            .Search(c => c.FullName, $"%{searchFilterValue}%")
            .Search(c => c.Id.ToString(), $"%{searchFilterValue}%")
            .Search(c => c.PreferredName, $"%{searchFilterValue}%")
            .Search(c => c.Salutation, $"%{searchFilterValue}%");
    }
}