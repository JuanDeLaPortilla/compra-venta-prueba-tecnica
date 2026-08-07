using CompraVenta.Domain.Common;
using CompraVenta.Domain.Entities;
using static CompraVenta.Domain.Common.Result;

namespace CompraVenta.Commerce.Core.Business.Products.Create;

public class CreateProductUseCase(CreateProductRequest request)
{
    public Product Product { get; set; } = new();

    public Result Execute()
    {
        var result = ValidateRequest();

        if (result.Code != ResultCode.Success) return result;

        Product = Product.Create(request.Name, request.BatchNumber);

        result = new Result
        {
            Code = ResultCode.Success,
            Message = "Producto creado correctamente."
        };
        
        return new CreateProductResponse(result, Product);
    }

    private Result ValidateRequest()
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return new Result
            {
                Code = ResultCode.BadRequest,
                Message = "El nombre es obligatorio."
            };
        }

        if (string.IsNullOrWhiteSpace(request.BatchNumber))
        {
            return new Result
            {
                Code = ResultCode.BadRequest,
                Message = "El número de lote es obligatorio."
            };
        }

        return new Result();
    }
}
