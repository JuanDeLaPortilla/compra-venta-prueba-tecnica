using CompraVenta.Auth.Infrastructure;
using CompraVenta.Persistence;
using CompraVenta.Security.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Auth API",
            Version = "v1",
        });
});

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


app.UseSwagger(options =>
{
    options.RouteTemplate = "swagger/{documentName}/swagger.json";

    // This tells Swagger that when served, all route paths must be prefixed for the gateway
    options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    {
        // Re-write the server paths so the gateway can route them correctly
        swaggerDoc.Servers = new List<OpenApiServer> { new() { Url = "/" } };
    });
});
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "Auth API v1");
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseGatewayValidation();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.MigrateDatabaseAsync();

app.Run();