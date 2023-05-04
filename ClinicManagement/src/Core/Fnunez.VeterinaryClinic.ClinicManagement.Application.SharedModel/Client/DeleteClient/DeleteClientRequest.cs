using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.DeleteClient;

public class DeleteClientRequest : BaseRequest
{
    public int Id { get; set; }
}