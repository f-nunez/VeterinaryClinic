using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate.ValueObjects;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.UnitTests.AppointmentAggregate;

public class AppointmentTests
{
    private readonly int _appointmentTypeId = 1;
    private readonly int _clientId = 2;
    private readonly int _clinicId = 3;
    private readonly DateTimeOffset _confirmOn = DateTimeOffset.UtcNow.AddDays(1);
    private readonly DateTimeOffset _dateRangeStartOn = DateTimeOffset.UtcNow;
    private readonly DateTimeOffset _dateRangeEndOn = DateTimeOffset.UtcNow.AddMinutes(1);
    private readonly string _description = "a";
    private readonly int _doctorId = 4;
    private readonly Guid _id = Guid.NewGuid();
    private readonly int _patientId = 5;
    private readonly int _roomId = 6;
    private readonly string _title = "b";

    [Fact]
    public void Constructor_AppointmentTypeId_SetsAppointmentTpeIdProperty()
    {
        // Arrange
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Equal(_appointmentTypeId, appointment.AppointmentTypeId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_AppointmentTypeIdIsLessThanOrEqualToZero_ThrowsArgumentException(
        int appointmentTypeId)
    {
        // Act
        Action actual = () => new Appointment
        (
            _id,
            appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_ClientId_SetsClientIdProperty()
    {
        // Arrange
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Equal(_clientId, appointment.ClientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ClientIdIsLessThanOrEqualToZero_ThrowsArgumentException(
        int clientId)
    {
        // Act
        Action actual = () => new Appointment
        (
            _id,
            _appointmentTypeId,
            clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_ClinicId_SetsClientIdProperty()
    {
        // Arrange
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Equal(_clinicId, appointment.ClinicId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ClinicIdIsLessThanOrEqualToZero_ThrowsArgumentException(
        int clinicId)
    {
        // Act
        Action actual = () => new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_ConfirmOn_SetsConfirmOnProperty()
    {
        // Arrange
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Equal(_confirmOn, appointment.ConfirmOn);
    }

    [Fact]
    public void Constructor_DateRange_SetsDateRangeProperty()
    {
        // Arrange
        var dateRange = new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn);

        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            dateRange,
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Equal(dateRange, appointment.DateRange);
    }

    [Fact]
    public void Constructor_DateRangeIsNull_ThrowsArgumentNullException()
    {
        // Act
        DateTimeOffsetRange? dateRange = null;

        Action actual = () => new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            dateRange!,
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Throws<ArgumentNullException>(actual);
    }

    [Fact]
    public void Constructor_Description_SetsDescriptionProperty()
    {
        // Arrange
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Equal(_description, appointment.Description);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_DescriptionIsEmpty_ThrowsArgumentException(
        string description)
    {
        // Act
        Action actual = () => new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_DoctorId_SetsDoctorIdProperty()
    {
        // Arrange
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Equal(_doctorId, appointment.DoctorId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_DoctorIdIsLessThanOrEqualToZero_ThrowsArgumentException(
        int doctorId)
    {
        // Act
        Action actual = () => new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_Id_SetsIdProperty()
    {
        // Arrange
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Equal(_id, appointment.Id);
    }

    [Fact]
    public void Constructor_IdIsEmpty_ThrowsArgumentException()
    {
        // Act
        var id = Guid.Empty;

        Action actual = () => new Appointment
        (
            id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_PatientId_SetsPatientIdProperty()
    {
        // Arrange
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Equal(_patientId, appointment.PatientId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_PatientIdIsLessThanOrEqualToZero_ThrowsArgumentException(
        int patientId)
    {
        // Act
        Action actual = () => new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_RoomId_SetsRoomIdProperty()
    {
        // Arrange
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Equal(_roomId, appointment.RoomId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_RoomIdIsLessThanOrEqualToZero_ThrowsArgumentException(
        int roomId)
    {
        // Act
        Action actual = () => new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_Title_SetsTitleProperty()
    {
        // Arrange
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Assert
        Assert.Equal(_title, appointment.Title);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_TitleIsEmpty_ThrowsArgumentException(
        string title)
    {
        // Act
        Action actual = () => new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            title,
            _confirmOn
        );

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void ResetConfirmOn_SetsConfirmOnAsNull()
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Act
        appointment.ResetConfirmOn();

        // Assert
        Assert.Null(appointment.ConfirmOn);
    }

    [Fact]
    public void UpdateAppointmentType_AppointmentTypeId_UpdatesAppointmentTypeIdProperty()
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        var appointmentTypeId = 2;

        // Act
        appointment.UpdateAppointmentType(appointmentTypeId);

        // Assert
        Assert.Equal(appointmentTypeId, appointment.AppointmentTypeId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void UpdateAppointmentType_AppointmentTypeIdIsLessThanOrEqualToZero_ThrowsArgumenException(
        int appointmentTypeId)
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Act
        Action actual = () => appointment.UpdateAppointmentType(appointmentTypeId);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateConfirmOn_IfConfirmOnIsNull_UpdatesConfirmOnProperty()
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            null
        );

        var confirmOn = DateTimeOffset.UtcNow;

        // Act
        appointment.UpdateConfirmOn(confirmOn);

        // Assert
        Assert.Equal(confirmOn, appointment.ConfirmOn);
    }

    [Fact]
    public void UpdateDateRange_DateRange_UpdatesDateRangeProperty()
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        var dateRange = new DateTimeOffsetRange(
            DateTimeOffset.UtcNow,
            DateTimeOffset.UtcNow.AddHours(1)
        );

        // Act
        appointment.UpdateDateRange(dateRange);

        // Assert
        Assert.Equal(dateRange, appointment.DateRange);
    }

    [Fact]
    public void UpdateDateRange_DateRangeIsNull_ThrowsArgumenNullException()
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        DateTimeOffsetRange? dateRange = null;


        // Act
        Action actual = () => appointment.UpdateDateRange(dateRange!);

        // Assert
        Assert.Throws<ArgumentNullException>(actual);
    }

    [Fact]
    public void UpdateDescription_Description_UpdatesDescriptionProperty()
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        var description = "aa";

        // Act
        appointment.UpdateDescription(description);

        // Assert
        Assert.Equal(description, appointment.Description);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateDescription_DescriptionIsEmpty_ThrowsArgumenException(
        string description)
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Act
        Action actual = () => appointment.UpdateDescription(description);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateDoctor_DoctorId_UpdatesDoctorIdProperty()
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        var doctorId = 2;

        // Act
        appointment.UpdateDoctor(doctorId);

        // Assert
        Assert.Equal(doctorId, appointment.DoctorId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void UpdateDoctor_DoctorIdIsLessThanOrEqualToZero_ThrowsArgumenException(
        int doctorId)
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Act
        Action actual = () => appointment.UpdateDoctor(doctorId);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateRoom_RoomId_UpdatesRoomIdProperty()
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        var roomId = 2;

        // Act
        appointment.UpdateRoom(roomId);

        // Assert
        Assert.Equal(roomId, appointment.RoomId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void UpdateRoom_RoomIdIsLessThanOrEqualToZero_ThrowsArgumenException(
        int roomId)
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Act
        Action actual = () => appointment.UpdateRoom(roomId);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateTitle_Title_UpdatesTitleProperty()
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        var title = "aa";

        // Act
        appointment.UpdateTitle(title);

        // Assert
        Assert.Equal(title, appointment.Title);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateTitle_TitleIsEmpty_ThrowsArgumenException(string title)
    {
        // Act
        var appointment = new Appointment
        (
            _id,
            _appointmentTypeId,
            _clientId,
            _clinicId,
            _doctorId,
            _patientId,
            _roomId,
            new DateTimeOffsetRange(_dateRangeStartOn, _dateRangeEndOn),
            _description,
            _title,
            _confirmOn
        );

        // Act
        Action actual = () => appointment.UpdateTitle(title);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }
}