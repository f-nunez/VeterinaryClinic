using Fnunez.VeterinaryClinic.SharedKernel.Application.Repositories;
using Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Application.IntegrationTests;

[CollectionDefinition(nameof(TestContextFixture))]
public class TestContextFixtureCollection : ICollectionFixture<TestContextFixture>
{
    // https://xunit.net/docs/shared-context
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

public class TestContextFixture
{
    private Respawner _checkpoint = null!;
    private IConfiguration _configuration = null!;
    private WebApplicationFactory<Program> _factory = null!;
    private IServiceScopeFactory _scopeFactory = null!;

    public TestContextFixture()
    {
        _factory = new CustomWebApplicationFactory();

        _scopeFactory = _factory.Services
            .GetRequiredService<IServiceScopeFactory>();

        _configuration = _factory
            .Services.GetRequiredService<IConfiguration>();

        _checkpoint = Respawner.CreateAsync(
            _configuration.GetConnectionString("DefaultConnection")!,
            new RespawnerOptions
            {
                TablesToIgnore = new Respawn.Graph.Table[]
                {
                    "__EFMigrationsHistory"
                }
            }
        ).GetAwaiter().GetResult();
    }

    public string GetUserIdAsManager()
    {
        return "9f79b45e-1ebe-4bb2-9d6f-e00da51b0848";
    }

    public string GetUserIdAsStaff()
    {
        return "ca59b781-77c7-4b66-a05c-2910c2cb5d1f";
    }

    #region Mediator
    public async Task<TResponse> SendAsync<TResponse>(
        IRequest<TResponse> request, string? userId = null)
    {
        var serviceScopeFactory = GetServiceScopeFactory(userId);

        using var scope = serviceScopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public async Task SendAsync(IBaseRequest request, string? userId = null)
    {
        var serviceScopeFactory = GetServiceScopeFactory(userId);

        using var scope = serviceScopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }
    #endregion

    #region UnitOfWork
    public async Task AddAsync<T>(T entity)
        where T : class, IAggregateRoot
    {
        using var scope = _scopeFactory.CreateScope();

        var unitOfWork = scope.ServiceProvider
            .GetRequiredService<IUnitOfWork>();

        await unitOfWork.Repository<T>().AddAsync(entity);

        await unitOfWork.CommitAsync();
    }

    public async Task<T?> GetByIdAsync<T>(object id)
        where T : class, IAggregateRoot
    {
        using var scope = _scopeFactory.CreateScope();

        var unitOfWork = scope.ServiceProvider
            .GetRequiredService<IUnitOfWork>();

        var entity = await unitOfWork.ReadRepository<T>().GetByIdAsync(id);

        return entity;
    }

    public async Task UpdateAsync<T>(T entity)
        where T : class, IAggregateRoot
    {
        using var scope = _scopeFactory.CreateScope();

        var unitOfWork = scope.ServiceProvider
            .GetRequiredService<IUnitOfWork>();

        await unitOfWork.Repository<T>().UpdateAsync(entity);

        await unitOfWork.CommitAsync();
    }
    #endregion

    private IServiceScopeFactory GetServiceScopeFactory(string? userId)
    {
        if (string.IsNullOrEmpty(userId))
            return _scopeFactory;

        var factory = new CustomWebApplicationFactory(userId);

        var scopeFactory = factory.Services
            .GetRequiredService<IServiceScopeFactory>();

        return scopeFactory;
    }
}