using System.Text.Json.Serialization;

namespace CompraVenta.Commerce.Core.Business.Products.Create
{
    public class CreateProductRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("batchNumber")]
        public string BatchNumber { get; set; } = string.Empty;
    }
}
