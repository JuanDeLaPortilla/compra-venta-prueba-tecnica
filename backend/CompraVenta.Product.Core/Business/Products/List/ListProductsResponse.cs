
using CompraVenta.Domain.Common;
using System.Text.Json.Serialization;

namespace CompraVenta.Commerce.Core.Business.Products.List;

public class ListProductsResponse(List<ProductResponse> data) : Result
{
    [JsonPropertyName("data")]
    public List<ProductResponse> Data = data;
}
