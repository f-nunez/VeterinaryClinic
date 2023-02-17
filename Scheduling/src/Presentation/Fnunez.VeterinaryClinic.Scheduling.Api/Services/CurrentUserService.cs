using System.Security.Claims;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Interfaces;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?
        .FindFirstValue(ClaimTypes.NameIdentifier);
}