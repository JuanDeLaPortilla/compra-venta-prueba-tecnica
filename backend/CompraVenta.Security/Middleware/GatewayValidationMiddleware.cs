using CompraVenta.Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using static CompraVenta.Domain.Common.Result;

namespace CompraVenta.Security.Middleware;

public class GatewayValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public GatewayValidationMiddleware(
        RequestDelegate next,
        IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Excluir Swagger
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }
        
        var internalKey =
            context.Request.Headers[GatewayConstants.InternalKeyHeaderName]
                .FirstOrDefault();
        
        var expectedKey =
            _configuration[GatewayConstants.InternalKeyValueKey];
        
        if (internalKey != expectedKey)
        {
            context.Response.StatusCode = (int)ResultCode.Unauthorized;

            await context.Response.WriteAsync(
                "Unauthorized Gateway Request");

            return;
        }
        
        await _next(context);
    }
}