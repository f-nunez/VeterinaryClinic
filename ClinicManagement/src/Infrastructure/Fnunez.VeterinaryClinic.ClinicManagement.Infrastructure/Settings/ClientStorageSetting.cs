using Fnunez.VeterinaryClinic.ClinicManagement.Application.Settings;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Settings;

public class ClientStorageSetting : IClientStorageSetting
{
    public string BasePath { get; set; } = null!;
}