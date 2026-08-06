using CompraVenta.Commerce.Core.Business.Products.Create;
using CompraVenta.Commerce.Core.Business.Products.List;
using CompraVenta.Commerce.Core.Business.Products.Update;
using CompraVenta.Commerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CompraVenta.Domain.Common.Result;

namespace CompraVenta.Commerce.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IUnitOfWork unitOfWork) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var products = await unitOfWork.Products.ListProductsAsync();

            var result = new ListProductsResponse(products);

            return ResultResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            var useCase = new CreateProductUseCase(request);
            var result = useCase.Execute();

            if (result.Code != ResultCode.Success)
            {
                return ResultResponse(result);
            }

            await unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await unitOfWork.AddAsync(useCase.Product);
            });

            return ResultResponse(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequest request)
        {
            var product = await unitOfWork.Products.GetAsync(id);

            var useCase = new UpdateProductUseCase(product, request);
            var result = useCase.Execute();

            if (result.Code != ResultCode.Success)
            {
                return ResultResponse(result);
            }

            await unitOfWork.SaveChangesInTransactionAsync();

            return ResultResponse(result);
        }
    }
}
