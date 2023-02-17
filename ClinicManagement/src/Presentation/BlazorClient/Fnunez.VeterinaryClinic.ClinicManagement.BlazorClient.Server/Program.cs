var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBlazorServerWebServices(builder.Configuration);

var app = builder.Build();

app.AddBlazorServerWebApplicationBuilder();

app.Run();
