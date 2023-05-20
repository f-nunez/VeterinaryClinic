using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Doctors.Queries.GetDoctorById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Doctor.GetDoctorById;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Doctors.Queries.GetDoctorById;

public class GetDoctorByIdQueryValidatorTests
{
    private readonly GetDoctorByIdQueryValidator _validator;

    public GetDoctorByIdQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Valdiation_IdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int id = 1;

        var request = new GetDoctorByIdRequest { Id = id };

        var query = new GetDoctorByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetDoctorByIdRequest.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Valdiation_IdIsLessThanOrEqualToZero_Fails(int id)
    {
        // Arrange
        var request = new GetDoctorByIdRequest { Id = id };

        var query = new GetDoctorByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetDoctorByIdRequest.Id);
    }
}