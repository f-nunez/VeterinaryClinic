using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Queries.GetAppointmentTypeById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.GetAppointmentTypeById;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.AppointmentTypes.Queries.GetAppointmentTypeById;

public class GetAppointmentTypeByIdQueryValidatorTests
{
    private readonly GetAppointmentTypeByIdQueryValidator _validator;

    public GetAppointmentTypeByIdQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_IdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int id = 1;

        var request = new GetAppointmentTypeByIdRequest { Id = id };

        var query = new GetAppointmentTypeByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentTypeByIdRequest.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_IdIsLessThanOrEqualToZero_Fails(int id)
    {
        // Arrange
        var request = new GetAppointmentTypeByIdRequest { Id = id };

        var query = new GetAppointmentTypeByIdQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentTypeByIdRequest.Id);
    }
}