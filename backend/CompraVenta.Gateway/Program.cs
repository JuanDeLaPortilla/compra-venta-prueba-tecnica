using CompraVenta.ApiGateway.Authentication;
using CompraVenta.ApiGateway.Extensions;
using CompraVenta.Domain.Constants;
using Microsoft.OpenApi.Models;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "CompraVenta Gateway API",
            Version = "v1"
        });
});

builder.Services
    .AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(
        builder.Configuration
            .GetSection(GatewayConstants.ReverseProxySectionKey))
    .AddTransforms(builderContext =>
    {
        builderContext.AddRequestTransform(transformContext =>
        {
            transformContext.ProxyRequest.Headers
                .Add(
                    GatewayConstants.InternalKeyHeaderName,
                    builder.Configuration.GetValue<string>(GatewayConstants.InternalKeyValueKey)
                );

            return ValueTask.CompletedTask;
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "Gateway API");
    
    options.SwaggerEndpoint(
        "/swagger/auth/v1/swagger.json",
        "Auth Service");
});

app.UseHttpsRedirection();

app.UseCustomMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();