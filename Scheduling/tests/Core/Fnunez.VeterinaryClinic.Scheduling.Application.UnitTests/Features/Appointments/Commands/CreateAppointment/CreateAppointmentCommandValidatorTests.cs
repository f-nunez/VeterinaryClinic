using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.CreateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.CreateAppointment;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommandValidatorTests
{
    private readonly CreateAppointmentCommandValidator _validator;

    public CreateAppointmentCommandValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_AppointmentTypeIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int appointmentTypeId = 1;

        var request = new CreateAppointmentRequest
        {
            AppointmentTypeId = appointmentTypeId
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.AppointmentTypeId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_AppointmentTypeIdIsLessThanOrEqualToZero_Fails(
        int appointmentTypeId)
    {
        // Arrange
        var request = new CreateAppointmentRequest
        {
            AppointmentTypeId = appointmentTypeId
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.AppointmentTypeId);
    }

    [Fact]
    public void Validation_ClientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clientId = 1;

        var request = new CreateAppointmentRequest
        {
            ClientId = clientId
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClientIdIsLessThanOrEqualToZero_Fails(int clientId)
    {
        // Arrange
        var request = new CreateAppointmentRequest
        {
            ClientId = clientId
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.ClientId);
    }

    [Fact]
    public void Validation_ClinicIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int clinicId = 1;

        var request = new CreateAppointmentRequest
        {
            ClinicId = clinicId
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.ClinicId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_ClinicIdIsLessThanOrEqualToZero_Fails(int clinicId)
    {
        // Arrange
        var request = new CreateAppointmentRequest
        {
            ClinicId = clinicId
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.ClinicId);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2000)]
    public void Validation_DescriptionHasCharactersBetweenOneAndTwoThousand_IsValid(
        int descriptionLength)
    {
        // Arrange
        var description = string.Empty;

        for (int i = 0; i < descriptionLength; i++)
            description += "a";

        var request = new CreateAppointmentRequest
        {
            Description = description
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.Description);
    }

    [Fact]
    public void Validation_DescriptionHasMoreThanTwoThousandCharacters_Fails()
    {
        // Arrange
        var description = string.Empty;

        for (int i = 0; i < 2001; i++)
            description += "a";

        var request = new CreateAppointmentRequest
        {
            Description = description
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.Description);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_DescriptionIsEmpty_Fails(string description)
    {
        // Arrange
        var request = new CreateAppointmentRequest
        {
            Description = description
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.Description);
    }

    [Fact]
    public void Validation_DoctorIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int doctorId = 1;

        var request = new CreateAppointmentRequest
        {
            DoctorId = doctorId
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.DoctorId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_DoctorIdIsLessThanOrEqualToZero_Fails(int doctorId)
    {
        // Arrange
        var request = new CreateAppointmentRequest
        {
            DoctorId = doctorId
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.DoctorId);
    }

    [Fact]
    public void Validation_EndOn_IsValid()
    {
        // Arrange
        var endOn = DateTimeOffset.UtcNow;

        var request = new CreateAppointmentRequest
        {
            EndOn = endOn
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.EndOn);
    }

    [Fact]
    public void Validation_EndOnIsEmpty_Fails()
    {
        // Arrange
        var request = new CreateAppointmentRequest();

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.EndOn);
    }

    [Fact]
    public void Validation_PatientIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int patientId = 1;

        var request = new CreateAppointmentRequest
        {
            PatientId = patientId
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.PatientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_PatientIdIsLessThanOrEqualToZero_Fails(int patientId)
    {
        // Arrange
        var request = new CreateAppointmentRequest
        {
            PatientId = patientId
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.PatientId);
    }

    [Fact]
    public void Validation_RoomIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int roomId = 1;

        var request = new CreateAppointmentRequest
        {
            RoomId = roomId
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.RoomId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_RoomIdIsLessThanOrEqualToZero_Fails(int roomId)
    {
        // Arrange
        var request = new CreateAppointmentRequest
        {
            RoomId = roomId
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.RoomId);
    }

    [Fact]
    public void Validation_StartOn_IsValid()
    {
        // Arrange
        var startOn = DateTimeOffset.UtcNow;

        var endOn = startOn.AddTicks(1);

        var request = new CreateAppointmentRequest
        {
            EndOn = endOn,
            StartOn = startOn
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.StartOn);
    }

    [Fact]
    public void Validation_StartOnIsEmpty_Fails()
    {
        // Arrange
        var endOn = DateTimeOffset.UtcNow;

        var request = new CreateAppointmentRequest
        {
            EndOn = endOn
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.StartOn);
    }

    [Fact]
    public void Validation_StartOnIsGreaterThanOrEqualToEndOn_Fails()
    {
        // Arrange
        var startOn = DateTimeOffset.UtcNow;

        var endOn = startOn.AddTicks(-1);

        var request = new CreateAppointmentRequest
        {
            EndOn = endOn,
            StartOn = startOn
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.StartOn);
    }

    [Fact]
    public void Validation_StartOnIsLessThanEndOn_IsValid()
    {
        // Arrange
        var startOn = DateTimeOffset.UtcNow;

        var endOn = startOn.AddTicks(1);

        var request = new CreateAppointmentRequest
        {
            EndOn = endOn,
            StartOn = startOn
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.StartOn);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(200)]
    public void Validation_TitleHasCharactersBetweenOneAndTwoHundred_IsValid(
        int titleLength)
    {
        // Arrange
        var title = string.Empty;

        for (int i = 0; i < titleLength; i++)
            title += "a";

        var request = new CreateAppointmentRequest
        {
            Title = title
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.Title);
    }

    [Fact]
    public void Validation_TitleHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var title = string.Empty;

        for (int i = 0; i < 201; i++)
            title += "a";

        var request = new CreateAppointmentRequest
        {
            Title = title
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.Title);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_TitleIsEmpty_Fails(string title)
    {
        // Arrange
        var request = new CreateAppointmentRequest
        {
            Title = title
        };

        var command = new CreateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.CreateAppointmentRequest.Title);
    }
}