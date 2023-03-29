namespace Fnunez.VeterinaryClinic.EmailService.Api.Services.Email;

public class EmailComposition
{
    public string? Body { get; set; }
    public bool IsBodyHtml { get; set; }
    public string? Subject { get; set; }
    public string? To { get; set; }
}