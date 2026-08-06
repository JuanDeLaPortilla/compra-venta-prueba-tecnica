using System.Text.Json.Serialization;
using CompraVenta.Domain.Common;

namespace CompraVenta.Commerce.Core.Business.Sales.List;

public class ListSalesResponse(List<SaleResponse> data) : Result
{
    [JsonPropertyName("data")]
    public List<SaleResponse> Data = data;
}