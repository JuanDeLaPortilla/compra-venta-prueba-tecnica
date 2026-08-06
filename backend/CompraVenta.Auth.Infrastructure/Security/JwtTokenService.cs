using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CompraVenta.Domain.Constants;
using CompraVenta.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CompraVenta.Auth.Infrastructure.Security;

public class JwtTokenService(IConfiguration configuration) : IJwtTokenService
{
    private const int DefaultTokenExpirationMinutes = 30;

    public string CreateToken(User user)
    {
        // Leer configuracion
        var jwtToken = configuration.GetValue(SecurityConstants.JwtTokenKey, string.Empty);
        var jwtTokenExpirationMinutes = configuration.GetValue(SecurityConstants.JwtTokenExpirationMinutesKey,
            DefaultTokenExpirationMinutes);

        if (string.IsNullOrEmpty(jwtToken))
        {
            throw new Exception("La configuración no contiene el valor Jwt:Token");
        }

        // Definir los claims del usuario en el token
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer32),
            new(ClaimTypes.Name, user.Username),
        };

        // Crear la clave de seguridad a partir del token secreto
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtToken));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtTokenExpirationMinutes), // Tiempo definido en variables de entorno
            signingCredentials: creds
        );

        // Generar el token como un string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}