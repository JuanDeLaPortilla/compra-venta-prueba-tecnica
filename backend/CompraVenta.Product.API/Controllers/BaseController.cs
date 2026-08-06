using CompraVenta.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using static CompraVenta.Domain.Common.Result;

namespace CompraVenta.Commerce.API.Controllers;

public class BaseController : Controller
{
    // GET
    protected IActionResult ResultResponse(Result? result = null)
    {
        result ??= new Result();

        return result.Code switch
        {
            ResultCode.Success => Ok(result),
            ResultCode.BadRequest => BadRequest(result),
            ResultCode.Unauthorized => Unauthorized(result),
            ResultCode.NotFound => NotFound(result),
            ResultCode.ServerError => StatusCode((int)ResultCode.ServerError, result),
            _ => Ok(result)
        };
    }
}