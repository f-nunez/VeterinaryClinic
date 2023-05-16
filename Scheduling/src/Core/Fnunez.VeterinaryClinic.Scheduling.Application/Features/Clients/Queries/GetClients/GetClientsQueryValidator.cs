using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Clients.Queries.GetClients;

public class GetClientsQueryValidator : AbstractValidator<GetClientsQuery>
{
    public GetClientsQueryValidator()
    {
        RuleFor(v => v.GetClientsRequest.DataGridRequest.Search)
            .MaximumLength(200).WithMessage("Search must not exceed 200 characters.");

        RuleFor(v => v.GetClientsRequest.DataGridRequest.Skip)
            .GreaterThanOrEqualTo(0).WithMessage("Skip must be greater than or equal to 0.");

        RuleFor(v => v.GetClientsRequest.DataGridRequest.Take)
            .GreaterThan(0).WithMessage("Take must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Take must be less than or equal to 100.");

        RuleFor(v => v.GetClientsRequest.EmailAddressFilterValue)
            .MaximumLength(200).WithMessage("EmailAddressFilterValue must not exceed 200 characters.");

        RuleFor(v => v.GetClientsRequest.FullNameFilterValue)
            .MaximumLength(200).WithMessage("FullNameFilterValue must not exceed 200 characters.");

        RuleFor(v => v.GetClientsRequest.IdFilterValue)
            .MaximumLength(200).WithMessage("IdFilterValue must not exceed 200 characters.");

        RuleFor(v => v.GetClientsRequest.PreferredNameFilterValue)
            .MaximumLength(200).WithMessage("PreferredNameFilterValue must not exceed 200 characters.");

        RuleFor(v => v.GetClientsRequest.SalutationFilterValue)
            .MaximumLength(200).WithMessage("SalutationFilterValue must not exceed 200 characters.");
    }
}