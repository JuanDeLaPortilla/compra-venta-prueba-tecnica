using CompraVenta.Commerce.Core.Interfaces;
using CompraVenta.Domain.Common;
using CompraVenta.Domain.Entities;
using static CompraVenta.Domain.Common.Result;
using static CompraVenta.Domain.Entities.Movement;

namespace CompraVenta.Commerce.Core.Business.Purchases.Create
{
    public class CreatePurchaseFacade(IUnitOfWork unitOfWork, CreatePurchaseRequest request)
    {
        private List<Product> _products = [];
        
        public async Task<Result> ExecuteAsync()
        {
            var result = await ValidateRequestAsync();

            if (result.Code != ResultCode.Success)
            {
                return result;
            }

            // Crear compra
            var productsDictionary = _products.ToDictionary(x => x.Id);

            var purchaseItems = request.Details
                .Select(x => new PurchaseItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                })
                .ToList();

            var purchase = Purchase.Create(purchaseItems, productsDictionary);

            // Registrar el movimiento
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

        private async Task<Result> ValidateRequestAsync()
        {
            if (request.Details.Count == 0)
            {
                return new Result
                {
                    Code = ResultCode.BadRequest,
                    Message = "Purchase must contain at least one product."
                };
            }
            
            // Obtener productos
            var productsIds = request.Details
                .Select(d => d.ProductId)
                .Distinct()
                .ToList();

            _products = (await unitOfWork.Products
                    .GetAllAsync(x => productsIds.Contains(x.Id)))
                .ToList();
            
            // Validar productos
            if (_products.Count != productsIds.Count)
            {
                return new Result
                {
                    Code = ResultCode.BadRequest,
                    Message = "Uno o más productos no existen."
                };
            }

            return new Result();
        }
    }
}
