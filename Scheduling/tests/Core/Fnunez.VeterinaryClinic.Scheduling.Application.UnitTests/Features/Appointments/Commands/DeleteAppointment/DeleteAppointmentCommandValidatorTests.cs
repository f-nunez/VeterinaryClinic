using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.DeleteAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.DeleteAppointment;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Appointments.Commands.DeleteAppointment;

public class DeleteAppointmentCommandValidatorTests
{
    private readonly DeleteAppointmentCommandValidator _validator;

    public DeleteAppointmentCommandValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_AppointmentId_IsValid()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();

        var request = new DeleteAppointmentRequest
        {
            AppointmentId = appointmentId
        };

        var command = new DeleteAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.DeleteAppointmentRequest.AppointmentId);
    }

    [Fact]
    public void Validation_AppointmentIdIsEmpty_Fails()
    {
        // Arrange
        var appointmentId = Guid.Empty;

        var request = new DeleteAppointmentRequest
        {
            AppointmentId = appointmentId
        };

        var command = new DeleteAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.DeleteAppointmentRequest.AppointmentId);
    }
}