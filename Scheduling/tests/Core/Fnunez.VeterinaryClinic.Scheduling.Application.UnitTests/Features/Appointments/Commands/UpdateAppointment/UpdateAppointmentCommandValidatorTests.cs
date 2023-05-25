using FluentValidation.TestHelper;
using Fnunez.VeterinaryClinic.Scheduling.Application.Features.Appointments.Commands.UpdateAppointment;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Appointment.UpdateAppointment;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.UnitTests.Features.Appointments.Commands.UpdateAppointment;

public class UpdateAppointmentCommandValidatorTests
{
    private readonly UpdateAppointmentCommandValidator _validator;

    public UpdateAppointmentCommandValidatorTests()
    {
        _validator = new();
    }

    [Fact]
    public void Validation_AppointmentId_IsValid()
    {
        // Arrange
        var appointmentId = Guid.NewGuid();

        var request = new UpdateAppointmentRequest
        {
            AppointmentId = appointmentId
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.AppointmentId);
    }

    [Fact]
    public void Validation_AppointmentIdIsEmpty_Fails()
    {
        // Arrange
        var appointmentId = Guid.Empty;

        var request = new UpdateAppointmentRequest
        {
            AppointmentId = appointmentId
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.AppointmentId);
    }

    [Fact]
    public void Validation_AppointmentTypeIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int appointmentTypeId = 1;

        var request = new UpdateAppointmentRequest
        {
            AppointmentTypeId = appointmentTypeId
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.AppointmentTypeId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_AppointmentTypeIdIsLessThanOrEqualToZero_Fails(
        int appointmentTypeId)
    {
        // Arrange
        var request = new UpdateAppointmentRequest
        {
            AppointmentTypeId = appointmentTypeId
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.AppointmentTypeId);
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

        var request = new UpdateAppointmentRequest
        {
            Description = description
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.Description);
    }

    [Fact]
    public void Validation_DescriptionHasMoreThanTwoThousandCharacters_Fails()
    {
        // Arrange
        var description = string.Empty;

        for (int i = 0; i < 2001; i++)
            description += "a";

        var request = new UpdateAppointmentRequest
        {
            Description = description
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.Description);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_DescriptionIsEmpty_Fails(string description)
    {
        // Arrange
        var request = new UpdateAppointmentRequest
        {
            Description = description
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.Description);
    }

    [Fact]
    public void Validation_DoctorIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int doctorId = 1;

        var request = new UpdateAppointmentRequest
        {
            DoctorId = doctorId
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.DoctorId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_DoctorIdIsLessThanOrEqualToZero_Fails(int doctorId)
    {
        // Arrange
        var request = new UpdateAppointmentRequest
        {
            DoctorId = doctorId
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.DoctorId);
    }

    [Fact]
    public void Validation_EndOn_IsValid()
    {
        // Arrange
        var endOn = DateTimeOffset.UtcNow;

        var request = new UpdateAppointmentRequest
        {
            EndOn = endOn
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.EndOn);
    }

    [Fact]
    public void Validation_EndOnIsEmpty_Fails()
    {
        // Arrange
        var request = new UpdateAppointmentRequest();

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.EndOn);
    }

    [Fact]
    public void Validation_RoomIdIsGreaterThanZero_IsValid()
    {
        // Arrange
        int roomId = 1;

        var request = new UpdateAppointmentRequest
        {
            RoomId = roomId
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.RoomId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validation_RoomIdIsLessThanOrEqualToZero_Fails(int roomId)
    {
        // Arrange
        var request = new UpdateAppointmentRequest
        {
            RoomId = roomId
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        //Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.RoomId);
    }

    [Fact]
    public void Validation_StartOn_IsValid()
    {
        // Arrange
        var startOn = DateTimeOffset.UtcNow;

        var endOn = startOn.AddTicks(1);

        var request = new UpdateAppointmentRequest
        {
            EndOn = endOn,
            StartOn = startOn
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.StartOn);
    }

    [Fact]
    public void Validation_StartOnIsEmpty_Fails()
    {
        // Arrange
        var endOn = DateTimeOffset.UtcNow;

        var request = new UpdateAppointmentRequest
        {
            EndOn = endOn
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.StartOn);
    }

    [Fact]
    public void Validation_StartOnIsGreaterThanOrEqualToEndOn_Fails()
    {
        // Arrange
        var startOn = DateTimeOffset.UtcNow;

        var endOn = startOn.AddTicks(-1);

        var request = new UpdateAppointmentRequest
        {
            EndOn = endOn,
            StartOn = startOn
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.StartOn);
    }

    [Fact]
    public void Validation_StartOnIsLessThanEndOn_IsValid()
    {
        // Arrange
        var startOn = DateTimeOffset.UtcNow;

        var endOn = startOn.AddTicks(1);

        var request = new UpdateAppointmentRequest
        {
            EndOn = endOn,
            StartOn = startOn
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.StartOn);
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

        var request = new UpdateAppointmentRequest
        {
            Title = title
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.Title);
    }

    [Fact]
    public void Validation_TitleHasMoreThanTwoHundredCharacters_Fails()
    {
        // Arrange
        var title = string.Empty;

        for (int i = 0; i < 201; i++)
            title += "a";

        var request = new UpdateAppointmentRequest
        {
            Title = title
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.Title);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validation_TitleIsEmpty_Fails(string title)
    {
        // Arrange
        var request = new UpdateAppointmentRequest
        {
            Title = title
        };

        var command = new UpdateAppointmentCommand(request);

        // Act
        var validationResult = _validator.TestValidate(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x =>
            x.UpdateAppointmentRequest.Title);
    }
}