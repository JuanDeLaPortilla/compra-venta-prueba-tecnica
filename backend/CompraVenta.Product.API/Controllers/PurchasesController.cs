using CompraVenta.Commerce.Core.Business.Purchases.Create;
using CompraVenta.Commerce.Core.Business.Purchases.List;
using CompraVenta.Commerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompraVenta.Commerce.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController(IUnitOfWork unitOfWork) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var purchases = await unitOfWork.Purchases.ListPurchasesAsync();

            var result = new ListPurchasesResponse(purchases);

            return ResultResponse(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePurchaseRequest request)
        {
            var facade = new CreatePurchaseFacade(unitOfWork, request);

            var result = await facade.ExecuteAsync();

            return ResultResponse(result);
        }
    }
}
