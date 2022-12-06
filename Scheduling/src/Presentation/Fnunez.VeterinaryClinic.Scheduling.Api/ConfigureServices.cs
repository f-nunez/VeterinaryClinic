using Fnunez.VeterinaryClinic.Scheduling.Api.Filters;
using Fnunez.VeterinaryClinic.Scheduling.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication AddWebApplicationBuilder(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseHealthChecks("/api/health");

        app.MapControllers();

        return app;
    }
}