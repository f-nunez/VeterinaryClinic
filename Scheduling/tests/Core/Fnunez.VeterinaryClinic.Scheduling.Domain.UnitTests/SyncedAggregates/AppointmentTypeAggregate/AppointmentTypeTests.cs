using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.UnitTests.SyncedAggregates.AppointmentTypeAggregate;

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

    [Fact]
    public void Constructor_Duration_SetsDurationProperty()
    {
        // Arrange
        var appointmentType = new AppointmentType(_name, _code, _duration);

        // Assert
        Assert.Equal(_duration, appointmentType.Duration);
    }

    [Fact]
    public void Constructor_Name_SetsNameProperty()
    {
        // Arrange
        var appointmentType = new AppointmentType(_name, _code, _duration);

        // Assert
        Assert.Equal(_name, appointmentType.Name);
    }
}