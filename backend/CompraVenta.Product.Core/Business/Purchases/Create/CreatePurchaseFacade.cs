using CompraVenta.Commerce.Core.Interfaces;
using CompraVenta.Domain.Common;
using CompraVenta.Domain.Entities;
using static CompraVenta.Domain.Common.Result;
using static CompraVenta.Domain.Entities.Movement;

namespace CompraVenta.Commerce.Core.Business.Purchases.Create
{
    public class CreatePurchaseFacade(IUnitOfWork unitOfWork, CreatePurchaseRequest request)
    {
        public async Task<Result> ExecuteAsync()
        {
            var result = ValidateRequest();

            if (result.Code != ResultCode.Success)
            {
                return result;
            }

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

            // Crear compra
            var productsDictionary = products.ToDictionary(x => x.Id);

            var purchaseItems = request.Details
                .Select(x => new PurchaseItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.Price
                })
                .ToList();

            var purchase = Purchase.Create(purchaseItems, productsDictionary);

            // Crear movimiento
            var movementItems = request.Details
                .Select(x => new MovementItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                })
                .ToList();

            var movement = Movement.Create(MovementType.InBound, movementItems);

            await unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await unitOfWork.AddAsync(purchase);

                movement.OriginDocumentId = purchase.Id;

                await unitOfWork.AddAsync(movement);
            });

            return new Result
            {
                Code = ResultCode.Success,
                Message = "Compra creada exitosamente."
            };
        }

        private Result ValidateRequest()
        {
            if (request.Details.Count == 0)
            {
                return new Result
                {
                    Code = ResultCode.BadRequest,
                    Message = "Purchase must contain at least one product."
                };
            }

            return new Result();
        }
    }
}
