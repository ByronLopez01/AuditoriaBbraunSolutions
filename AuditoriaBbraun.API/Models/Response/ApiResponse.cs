using System.Text.Json.Serialization;

namespace AuditoriaBbraun.API.Models.Response
{
    /// <summary>
    /// Respuesta estándar: { "code": 200, "msg": "OK", "data": { ... } }
    /// </summary>
    public class ApiResponse<T>
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T? data = default, string msg = "OK") =>
            new() { Code = 200, Msg = msg, Data = data };

        public static ApiResponse<T> Fail(int code, string msg) =>
            new() { Code = code, Msg = msg, Data = default };
    }
}
