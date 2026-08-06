using System.Text.Json.Serialization;

namespace CompraVenta.Commerce.Core.Business.Kardex.ListProductMovements;

public class ProductMovementResponse
{
    [JsonPropertyName("registrationDate")]
    public DateTime RegistrationDate { get; set; }

    [JsonPropertyName("movementType")] 
    public string MovementType { get; set; } = string.Empty;
    
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}