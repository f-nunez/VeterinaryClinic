using FluentValidation;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Clients.Queries.GetClientsFilterId;

public class GetClientsFilterIdQueryValidator
    : AbstractValidator<GetClientsFilterIdQuery>
{
    public GetClientsFilterIdQueryValidator()
    {
        RuleFor(v => v.GetClientsFilterIdRequest.IdFilterValue)
            .NotNull().WithMessage("IdFilterValue is required.")
            .NotEmpty().WithMessage("IdFilterValue is required.");
    }
}