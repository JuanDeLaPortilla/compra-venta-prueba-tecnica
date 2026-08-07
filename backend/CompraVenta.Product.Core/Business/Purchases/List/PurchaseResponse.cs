using System.Text.Json.Serialization;

namespace CompraVenta.Commerce.Core.Business.Purchases.List;

public class PurchaseResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("registrationDate")]
    public DateTime RegistrationDate { get; set; }
    
    [JsonPropertyName("subTotal")]
    public decimal SubTotal { get; set; }
    
    [JsonPropertyName("taxAmount")]
    public decimal TaxAmount { get; set; }
    
    [JsonPropertyName("totalAmount")]
    public decimal TotalAmount { get; set; }
}