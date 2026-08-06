using CompraVenta.Domain.Entities;
using CompraVenta.Util.PasswordManager;
using static CompraVenta.Domain.Common.Result;

namespace CompraVenta.Auth.Core.Business.Auth.Login;

public class LoginUseCase(User? user, LoginRequest request)
{
    public LoginResult Execute()
    {
        var result = ValidateRequest();

        if (result.Code != ResultCode.Success) return result;

        return new LoginResult
        {
            Code = ResultCode.Success,
            Message = "Inicio de sesión exitoso."
        };
    }

    private LoginResult ValidateRequest()
    {
        if (string.IsNullOrWhiteSpace(request.Username) 
            || string.IsNullOrWhiteSpace(request.Password) || user == null)
        {
            return new LoginResult
            {
                Code = ResultCode.BadRequest,
                Message = "Usuario o contraseña incorrectos."
            };
        }

        var encryptedPassword = PasswordManager.EncodePassword(request.Password);
        
        if (!user.PasswordHash.Equals(encryptedPassword, StringComparison.CurrentCultureIgnoreCase))
        {
            return new LoginResult
            {
                Code = ResultCode.BadRequest,
                Message = "Correo o contraseña incorrectos."
            };
        }
        
        return new LoginResult();
    }
}