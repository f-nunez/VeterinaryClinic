using AutoMapper;
using Fnunez.VeterinaryClinic.SchedulingNotifications.Application.Services.NotificationEngine.Mappings;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.UnitTests.Services.NotificationEngine.Mappings;

public class AppointmentProfileTests
{
    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        // Arrange
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AppointmentProfile>();
        });

        // Act
        var mapper = new Mapper(mapperConfiguration);

        //Assert
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}