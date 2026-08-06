using System.Text.Json.Serialization;
using CompraVenta.Domain.Common;

namespace CompraVenta.Commerce.Core.Business.Kardex.ListProductMovements;

public class ListProductMovementsResponse(List<ProductMovementResponse> data) : Result
{
    [JsonPropertyName("data")]
    public List<ProductMovementResponse> Data = data;
}