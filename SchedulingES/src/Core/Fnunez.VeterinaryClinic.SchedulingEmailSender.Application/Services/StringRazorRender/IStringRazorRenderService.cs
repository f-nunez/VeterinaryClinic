namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Application.Services.StringRazorRender;

public interface IStringRazorRenderService
{
    Task<string> RenderRazorToStringAsync(string viewName, object? model, bool isMainPage = true);
}