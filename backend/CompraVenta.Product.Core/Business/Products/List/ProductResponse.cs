using System.Text.Json.Serialization;

namespace CompraVenta.Commerce.Core.Business.Products.List;

public class ProductResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("batchNumber")]
    public string BatchNumber { get; set; } = string.Empty;

    [JsonPropertyName("cost")]
    public decimal Cost { get; set; }

    [JsonPropertyName("salePrice")]
    public decimal SalePrice { get; set; }

    [JsonPropertyName("stock")]
    public int Stock { get; set; }
}
