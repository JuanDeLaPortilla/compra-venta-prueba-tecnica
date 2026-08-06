using System.Text.Json.Serialization;

namespace CompraVenta.Commerce.Core.Business.Kardex.List;

public class KardexResponse
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }
    
    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;
    
    [JsonPropertyName("stock")]
    public int Stock { get; set; }
    
    [JsonPropertyName("cost")]
    public decimal Cost { get; set; }
    
    [JsonPropertyName("salePrice")]
    public decimal SalePrice { get; set; }
}