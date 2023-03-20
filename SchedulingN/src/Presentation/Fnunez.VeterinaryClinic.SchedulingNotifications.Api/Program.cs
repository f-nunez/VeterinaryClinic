var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddWebServices(builder.Configuration);

var app = builder.Build();

app.AddWebApplicationBuilder();

app.Run();
