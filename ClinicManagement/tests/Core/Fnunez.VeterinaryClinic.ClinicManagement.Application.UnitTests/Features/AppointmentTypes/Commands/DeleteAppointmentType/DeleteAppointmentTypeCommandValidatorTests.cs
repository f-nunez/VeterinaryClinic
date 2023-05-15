using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.AppointmentTypes.Commands.DeleteAppointmentType;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.AppointmentType.DeleteAppointmentType;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.UnitTests.Features.AppointmentTypes.Commands.DeleteAppointmentType;

public class DeleteAppointmentTypeCommandValidatorTests
{
    private readonly DeleteAppointmentTypeCommandValidator _validator;

    public DeleteAppointmentTypeCommandValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_IdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int id = 1;

        var request = new DeleteAppointmentTypeRequest { Id = id };

        var command = new DeleteAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.DeleteAppointmentTypeRequest.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_IdIsLessThanOrEqualToZero_Fails(int id)
    {
        // Arrange
        var request = new DeleteAppointmentTypeRequest { Id = id };

        var command = new DeleteAppointmentTypeCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.DeleteAppointmentTypeRequest.Id);
    }
}