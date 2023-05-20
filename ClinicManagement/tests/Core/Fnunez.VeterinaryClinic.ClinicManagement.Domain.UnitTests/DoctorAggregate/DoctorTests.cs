using Fnunez.VeterinaryClinic.ClinicManagement.Domain.DoctorAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Domain.UnitTests.DoctorAggregate;

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

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_FullNameIsEmpty_ThrowsArgumentException(
        string fullName)
    {
        // Act
        Action actual = () => new Doctor(fullName);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }

    [Fact]
    public void UpdateFullName_FullName_UpdatesFullNameProperty()
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        doctor.UpdateFullName(_fullName);

        // Assert
        Assert.Equal(_fullName, doctor.FullName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateFullName_FullNameIsEmpty_ThrowsArgumentException(
        string fullName)
    {
        // Arrange
        var doctor = new Doctor();

        // Act
        Action actual = () => doctor.UpdateFullName(fullName);

        // Assert
        Assert.Throws<ArgumentException>(actual);
    }
}