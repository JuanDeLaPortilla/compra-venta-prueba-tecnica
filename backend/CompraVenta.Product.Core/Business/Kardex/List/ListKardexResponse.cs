using System.Text.Json.Serialization;
using CompraVenta.Domain.Common;

namespace CompraVenta.Commerce.Core.Business.Kardex.List;

public class ListKardexResponse(List<KardexResponse> data) : Result
{
    [JsonPropertyName("data")] 
    public List<KardexResponse> Data = data;
}