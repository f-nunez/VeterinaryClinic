using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Queries.GetAppointments;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.GetAppointments;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Appointments.Queries.GetAppointments;

public class GetAppointmentsQueryValidatorTests
{
    private readonly GetAppointmentsQueryValidator _validator;

    public GetAppointmentsQueryValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_ClientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clientId = 1;

        var request = new GetAppointmentsRequest
        {
            ClientId = clientId
        };

        var query = new GetAppointmentsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentsRequest.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClientIdIsLessThanOrEqualToZero_Fails(int clientId)
    {
        // Arrange
        var request = new GetAppointmentsRequest
        {
            ClientId = clientId
        };

        var query = new GetAppointmentsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentsRequest.ClientId);
    }

    [Fact]
    public void Validation_ClinicIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clinicId = 1;

        var request = new GetAppointmentsRequest
        {
            ClinicId = clinicId
        };

        var query = new GetAppointmentsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentsRequest.ClinicId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClinicIdIsLessThanOrEqualToZero_Fails(int clinicId)
    {
        // Arrange
        var request = new GetAppointmentsRequest
        {
            ClinicId = clinicId
        };

        var query = new GetAppointmentsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentsRequest.ClinicId);
    }

    [Fact]
    public void Validation_EndOn_IsValid()
    {
        // Arrange
        var endOn = DateTimeOffset.UtcNow;

        var request = new GetAppointmentsRequest
        {
            EndOn = endOn
        };

        var query = new GetAppointmentsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentsRequest.EndOn);
    }

    [Fact]
    public void Validation_EndOnIsEmpty_Fails()
    {
        // Arrange
        var request = new GetAppointmentsRequest();

        var query = new GetAppointmentsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentsRequest.EndOn);
    }

    [Fact]
    public void Validation_PatientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int patientId = 1;

        var request = new GetAppointmentsRequest
        {
            PatientId = patientId
        };

        var query = new GetAppointmentsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentsRequest.PatientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_PatientIdIsLessThanOrEqualToZero_Fails(
        int patientId)
    {
        // Arrange
        var request = new GetAppointmentsRequest
        {
            PatientId = patientId
        };

        var query = new GetAppointmentsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentsRequest.PatientId);
    }

    [Fact]
    public void Validation_StartOn_IsValid()
    {
        // Arrange
        var startOn = DateTimeOffset.UtcNow;

        var endOn = startOn.AddTicks(1);

        var request = new GetAppointmentsRequest
        {
            EndOn = endOn,
            StartOn = startOn
        };

        var query = new GetAppointmentsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentsRequest.StartOn);
    }

    [Fact]
    public void Validation_StartOnIsEmpty_Fails()
    {
        // Arrange
        var endOn = DateTimeOffset.UtcNow;

        var request = new GetAppointmentsRequest
        {
            EndOn = endOn
        };

        var query = new GetAppointmentsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentsRequest.StartOn);
    }

    [Fact]
    public void Validation_StartOnIsGreaterThanOrEqualToEndOn_Fails()
    {
        // Arrange
        var startOn = DateTimeOffset.UtcNow;

        var endOn = startOn.AddTicks(-1);

        var request = new GetAppointmentsRequest
        {
            EndOn = endOn,
            StartOn = startOn
        };

        var query = new GetAppointmentsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.GetAppointmentsRequest.StartOn);
    }

    [Fact]
    public void Validation_StartOnIsLessThanEndOn_IsValid()
    {
        // Arrange
        var startOn = DateTimeOffset.UtcNow;

        var endOn = startOn.AddTicks(1);

        var request = new GetAppointmentsRequest
        {
            EndOn = endOn,
            StartOn = startOn
        };

        var query = new GetAppointmentsQuery(request);

        // Act
        var validationResult = _validator.TestValidate(query);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.GetAppointmentsRequest.StartOn);
    }
}