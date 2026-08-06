using System.Text.Json.Serialization;

namespace CompraVenta.Commerce.Core.Business.Purchases.Create
{
    public class CreatePurchaseDetailRequest
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }
    }
}
