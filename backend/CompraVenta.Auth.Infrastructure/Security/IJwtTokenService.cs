using CompraVenta.Domain.Entities;

namespace CompraVenta.Auth.Infrastructure.Security;

public interface IJwtTokenService
{
    string CreateToken(User user);
}