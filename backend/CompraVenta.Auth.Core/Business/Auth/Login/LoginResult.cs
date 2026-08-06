using CompraVenta.Domain.Common;

namespace CompraVenta.Auth.Core.Business.Auth.Login;

public class LoginResult : Result
{
    public string Token { get; set; } = string.Empty;
}