using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Domain.UnitTests.SyncedAggregates.DoctorAggregate;

public class DoctorTests
{
    private readonly string _fullName = "a";

    [Fact]
    public void Constructor_FullName_SetsFullNameProperty()
    {
        // Arrange
        var doctor = new Doctor(_fullName);

        // Assert
        Assert.Equal(_fullName, doctor.FullName);
    }
}