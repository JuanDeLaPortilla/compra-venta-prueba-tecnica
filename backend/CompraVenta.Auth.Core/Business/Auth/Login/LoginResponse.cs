using CompraVenta.Domain.Common;

namespace CompraVenta.Auth.Core.Business.Auth.Login;

public class LoginResponse : Result
{
    public string Token { get; set; } = string.Empty;

    public LoginResponse(ResultCode resultCode, string? message, string token)
    {
        Code = resultCode;
        Message = message;
        Token = token;
    }
}