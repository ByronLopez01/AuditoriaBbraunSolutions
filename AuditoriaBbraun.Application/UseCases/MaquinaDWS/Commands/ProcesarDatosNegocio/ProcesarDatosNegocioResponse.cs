using System.Text.Json.Serialization;

namespace AuditoriaBbraun.Application.UseCases.MaquinaDWS.Commands.ProcesarDatosNegocio
{
    public class ProcesarDatosNegocioResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public DatosRespuesta? Data { get; set; }

        public class DatosRespuesta
        {
            [JsonPropertyName("roller")]
            public string? Roller { get; set; }
        }

        public static ProcesarDatosNegocioResponse Ok(string? roller = null) => new()
        {
            Code = 200,
            Msg = "OK",
            Data = new DatosRespuesta { Roller = roller ?? "router0" }
        };

        public static ProcesarDatosNegocioResponse Error(int code, string message) => new()
        {
            Code = code,
            Msg = message,
            Data = null
        };
    }
}
