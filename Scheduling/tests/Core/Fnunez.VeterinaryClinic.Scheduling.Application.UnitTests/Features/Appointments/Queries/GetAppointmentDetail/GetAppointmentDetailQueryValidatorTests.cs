using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointmentDetail;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointmentDetail;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Appointments.Queries.GetAppointmentDetail;

public class GetAppointmentDetailQueryValidatorTests
{
    private readonly GetAppointmentDetailQueryValidator _validator;

    public GetAppointmentDetailQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_AppointmentId_IsValid()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();

        var request = new GetAppointmentDetailRequest
        {
            AppointmentId = appointmentId
        };

        var query = new GetAppointmentDetailQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentDetailRequest.AppointmentId);
    }

    [Fact]
    public void Validation_AppointmentIdIsEmpty_Fails()
    {
        // Arrange
        var appointmentId = Guid.Empty;

        var request = new GetAppointmentDetailRequest
        {
            AppointmentId = appointmentId
        };

        var query = new GetAppointmentDetailQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentDetailRequest.AppointmentId);
    }
}