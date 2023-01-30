using FluentValidation;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorById;

public class GetDoctorByIdQueryValidator : AbstractValidator<GetDoctorByIdQuery>
{
    public GetDoctorByIdQueryValidator()
    {
        RuleFor(v => v.GetDoctorByIdRequest.Id)
            .GreaterThan(0).WithMessage("Id is required.");
    }
}