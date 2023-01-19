using Fnunez.VeterinaryClinic.ClinicManagement.Application.Interfaces.Settings;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Settings;

public class ClientStorageSetting : IClientStorageSetting
{
    public string BasePath { get; set; } = null!;
}