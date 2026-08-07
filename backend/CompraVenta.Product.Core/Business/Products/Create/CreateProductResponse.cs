using System.Text.Json.Serialization;
using CompraVenta.Domain.Common;
using CompraVenta.Domain.Entities;

namespace CompraVenta.Commerce.Core.Business.Products.Create;

public class CreateProductResponse() : Result
{
    [JsonPropertyName("data")]
    public Product Data { get; set; }

    public CreateProductResponse(Result result, Product data) : this()
    {
        Code = result.Code;
        Message = result.Message;
        Data = data;
    }
}