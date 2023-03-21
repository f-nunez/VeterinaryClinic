using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Server.Controllers;

[Route("api/[controller]")]
public class UserController : Controller
{
    [AllowAnonymous]
    [HttpGet("GetAccessToken")]
    public async Task<string> GetAccessToken()
    {
        var userToken = await HttpContext.GetUserAccessTokenAsync();

        return userToken.AccessToken ?? string.Empty;
    }
}