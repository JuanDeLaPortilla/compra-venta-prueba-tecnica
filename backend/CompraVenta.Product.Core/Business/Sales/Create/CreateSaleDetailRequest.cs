using System.Text.Json.Serialization;

namespace CompraVenta.Commerce.Core.Business.Sales.Create;

public class CreateSaleDetailRequest
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("unitPrice")]
    public decimal UnitPrice { get; set; }
}