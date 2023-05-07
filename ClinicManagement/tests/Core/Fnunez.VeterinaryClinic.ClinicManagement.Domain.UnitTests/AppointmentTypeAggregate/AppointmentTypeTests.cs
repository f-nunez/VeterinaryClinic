using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.UnitTests.AppointmentTypeAggregate;

public class AppointmentTypeTests
{
    private readonly string _code = "a";
    private readonly int _duration = 1;
    private readonly string _name = "a";

    [Fact]
    public void Constructor_Code_SetsCodeProperty()
    {
        // Arrange
        var appointmentType = new AppointmentType(_name, _code, _duration);

        // Assert
        Assert.Equal(_code, appointmentType.Code);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_CodeIsEmpty_ThrowsArgumentException(string code)
    {
        // Act
        Action actual = () => new AppointmentType(_name, code, _duration);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_Duration_SetsDurationProperty()
    {
        // Arrange
        var appointmentType = new AppointmentType(_name, _code, _duration);

        // Assert
        Assert.Equal(_duration, appointmentType.Duration);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_DurationIsLessOrEqualsThanZero_ThrowsArgumentException(
        int duration)
    {
        // Act
        Action actual = () => new AppointmentType(_name, _code, duration);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void Constructor_Name_SetsNameProperty()
    {
        // Arrange
        var appointmentType = new AppointmentType(_name, _code, _duration);

        // Assert
        Assert.Equal(_name, appointmentType.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_NameIsEmpty_ThrowsArgumentException(string name)
    {
        // Act
        Action actual = () => new AppointmentType(name, _code, _duration);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateCode_Code_UpdatesCodeProperty()
    {
        // Arrange
        var appointmentType = new AppointmentType();

        // Act
        appointmentType.UpdateCode(_code);

        // Assert
        Assert.Equal(_code, appointmentType.Code);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateCode_CodeIsEmpty_ThrowsArgumentException(string code)
    {
        // Arrange
        var appointmentType = new AppointmentType();

        // Act
        Action actual = () => appointmentType.UpdateCode(code);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateDuration_Duration_UpdatesDurationProperty()
    {
        // Arrange
        var appointmentType = new AppointmentType();

        // Act
        appointmentType.UpdateDuration(_duration);

        // Assert
        Assert.Equal(_duration, appointmentType.Duration);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void UpdateDuration_DurationIsLessOrEqualsThanZero_ThrowsArgumentException(
        int duration)
    {
        // Arrange
        var appointmentType = new AppointmentType();

        // Act
        Action actual = () => appointmentType.UpdateDuration(duration);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateName_Name_UpdatesNameProperty()
    {
        // Arrange
        var appointmentType = new AppointmentType();

        // Act
        appointmentType.UpdateName(_name);

        // Assert
        Assert.Equal(_name, appointmentType.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateName_NameIsEmpty_ThrowsArgumentException(string name)
    {
        // Arrange
        var appointmentType = new AppointmentType();

        // Act
        Action actual = () => appointmentType.UpdateName(name);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }
}