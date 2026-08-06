using CompraVenta.Security.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CompraVenta.Security.Extensions;

public static class GatewayValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseGatewayValidation(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<GatewayValidationMiddleware>();
    }
}