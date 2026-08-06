using CompraVenta.Auth.Core.Business.Auth.Login;
using CompraVenta.Auth.Infrastructure.Repositories;
using CompraVenta.Auth.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using static CompraVenta.Domain.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace CompraVenta.Auth.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService) : BaseController
{
    [AllowAnonymous]
    [HttpPost("public/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await unitOfWork.Users.GetByUsername(request.Username);
        
        var useCase = new LoginUseCase(user, request);
        var result = useCase.Execute();

        if (result.Code != ResultCode.Success)
        {
            return ResultResponse(result);
        }

        var token = jwtTokenService.CreateToken(user!);
        
        var loginReponse = new LoginResponse(result.Code, result.Message, token);
        
        return ResultResponse(loginReponse);
    }
}