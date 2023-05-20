using System.Security.Claims;
using Fnunez.VeterinaryClinic.Scheduling.Api.Services;
using Fnunez.VeterinaryClinic.Scheduling.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Fnunez.VeterinaryClinic.Scheduling.Api.UnitTests.Services;

public class CurrentUserServiceTests
{
    [Fact]
    public void UserId_NameIdentifierExistsInClaimsPrincipal_ReturnsUserId()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        };

        var claimsIdentity = new ClaimsIdentity(claims);

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var httpContext = new DefaultHttpContext();

        httpContext.User = claimsPrincipal;

        var httpContextAccessor = new HttpContextAccessor();

        httpContextAccessor.HttpContext = httpContext;

        ICurrentUserService currentUserService = new CurrentUserService(
            httpContextAccessor);

        // Act
        var actual = currentUserService.UserId;

        // Assert
        Assert.Equal(userId, actual);
    }

    [Fact]
    public void UserId_NameIdentifierDoesNotExistsInClaimsPrincipal_ReturnsNull()
    {
        // Arrange
        string? userId = null;

        var httpContext = new DefaultHttpContext();

        var httpContextAccessor = new HttpContextAccessor();

        httpContextAccessor.HttpContext = httpContext;

        ICurrentUserService currentUserService = new CurrentUserService(
            httpContextAccessor);

        // Act
        var actual = currentUserService.UserId;

        // Assert
        Assert.Equal(userId, actual);
    }
}