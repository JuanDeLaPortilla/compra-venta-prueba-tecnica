using System.Text.Json.Serialization;

namespace CompraVenta.Domain.Common;

public class Result
{
    public ResultCode Code { get; set; } = ResultCode.Success;
    public string? Message { get; set; }
    
    public enum ResultCode
    {
        Success = 200,
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        ServerError = 500
    }
}