using CompraVenta.Domain.Common;
using CompraVenta.Domain.Entities;
using static CompraVenta.Domain.Common.Result;

namespace CompraVenta.Commerce.Core.Business.Products.Update;

public class UpdateProductUseCase(Product? product, UpdateProductRequest request)
{
    public Product? Product { get; set; } = product;

    public Result Execute()
    {
        var result = ValidateRequest();

        if (result.Code != ResultCode.Success) return result;

        Product!.Update(request.Name, request.BatchNumber);

        return new Result
        {
            Code = ResultCode.Success,
            Message = "Producto actualizado correctamente."
        };
    }

    private Result ValidateRequest()
    {
        if (Product == null)
        {
            return new Result
            {
                Code = ResultCode.NotFound,
                Message = "El producto no existe."
            };
        }

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
