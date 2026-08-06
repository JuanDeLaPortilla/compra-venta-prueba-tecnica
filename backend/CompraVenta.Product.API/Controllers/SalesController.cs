using CompraVenta.Commerce.Core.Business.Sales.Create;
using CompraVenta.Commerce.Core.Business.Sales.List;
using CompraVenta.Commerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompraVenta.Commerce.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController(IUnitOfWork unitOfWork) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var sales = await unitOfWork.Sales.ListSalesAsync();

            var result = new ListSalesResponse(sales);

            return ResultResponse(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleRequest request)
        {
            var facade = new CreateSaleFacade(unitOfWork, request);

            var result = await facade.ExecuteAsync();

            return ResultResponse(result);
        }
    }
}
