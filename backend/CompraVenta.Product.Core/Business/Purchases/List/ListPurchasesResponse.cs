using System.Text.Json.Serialization;
using CompraVenta.Domain.Common;

namespace CompraVenta.Commerce.Core.Business.Purchases.List;

public class ListPurchasesResponse(List<PurchaseResponse> data) : Result
{
    [JsonPropertyName("data")]
    public List<PurchaseResponse> Data { get; set; } = data;
}