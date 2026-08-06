using System.Text.Json.Serialization;

namespace CompraVenta.Commerce.Core.Business.Purchases.Create
{
    public class CreatePurchaseRequest
    {
        [JsonPropertyName("details")]
        public List<CreatePurchaseDetailRequest> Details { get; set; } = [];
    }
}
