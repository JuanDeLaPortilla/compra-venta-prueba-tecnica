using CompraVenta.Commerce.Core.Business.Kardex.List;
using CompraVenta.Commerce.Core.Business.Kardex.ListProductMovements;
using CompraVenta.Commerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompraVenta.Commerce.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class KardexController(IUnitOfWork unitOfWork) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var list = await unitOfWork.Movements.ListKardexAsync();

        var result = new ListKardexResponse(list);
        
        return ResultResponse(result);
    }

    [HttpGet]
    [Route("{productId:int}/movements")]
    public async Task<IActionResult> ListProductMovements([FromRoute] int productId)
    {
        var list = await unitOfWork.Movements.ListProductMovementsAsync(productId);

        var result = new ListProductMovementsResponse(list);
        
        return ResultResponse(result);
    }
}