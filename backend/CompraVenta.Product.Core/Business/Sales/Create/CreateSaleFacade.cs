using CompraVenta.Commerce.Core.Interfaces;
using CompraVenta.Domain.Common;
using CompraVenta.Domain.Entities;
using static CompraVenta.Domain.Common.Result;
using static CompraVenta.Domain.Entities.Movement;

namespace CompraVenta.Commerce.Core.Business.Sales.Create;

public class CreateSaleFacade(IUnitOfWork unitOfWork, CreateSaleRequest request)
{
    public async Task<Result> ExecuteAsync()
    {
        var result = await ValidateRequestAsync();

        if (result.Code != ResultCode.Success)
        {
            return result;
        }

        // Crear venta
        var saleItems = request.Details
            .Select(x => new SaleItem
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice
            })
            .ToList();

        var sale = Sale.Create(saleItems);
        
        // Registrar el movimiento
        var movementItems = request.Details
            .Select(x => new MovementItem
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            })
            .ToList();
        
        var movement = Movement.Create(MovementType.OutBound, movementItems);
        
        await unitOfWork.ExecuteTransactionAsync(async () =>
        {
            await unitOfWork.AddAsync(sale);

            movement.OriginDocumentId = sale.Id;

            await unitOfWork.AddAsync(movement);
        });

        return new Result
        {
            Code = ResultCode.Success,
            Message = "Venta creada exitosamente."
        };
    }

    private async Task<Result> ValidateRequestAsync()
    {
        // Obtener productos
        var productsIds = request.Details
            .Select(d => d.ProductId)
            .Distinct()
            .ToList();

        var products = (await unitOfWork.Products
                .GetAllAsync(x => productsIds.Contains(x.Id)))
            .ToList();

        // Validar productos
        if (products.Count != productsIds.Count)
        {
            return new Result
            {
                Code = ResultCode.BadRequest,
                Message = "Uno o más productos no existen."
            };
        }

        var stock = await unitOfWork.Products
            .GetStockByProductsAsync(productsIds);

        foreach (var detail in request.Details)
        {
            var available = stock.GetValueOrDefault(detail.ProductId);

            if (available < detail.Quantity)
            {
                return new Result
                {
                    Code = ResultCode.BadRequest,
                    Message = $"No hay stock suficiente para el producto {detail.ProductId}."
                };
            }
        }

        return new Result();
    }
}