using System.Text.Json.Serialization;

namespace CompraVenta.Commerce.Core.Business.Sales.Create;

public class CreateSaleRequest
{
    [JsonPropertyName("details")] 
    public List<CreateSaleDetailRequest> Details { get; set; } = [];
}