namespace AuditoriaBbraun.API.Models.Response
{
    // Respuesta para el endpoint /api/dispositivos/procesar-datos
    // Según documentación DWSSystem: { "code": 200, "msg": "OK", "data": { "roller": "router1" } }
    public class DatosNegocioResponse
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
        public DatosRespuesta Datos { get; set; }

        public class DatosRespuesta
        {
            public string Rodillo { get; set; }
        }

        public static DatosNegocioResponse Exitoso(string rodillo = "router0")
        {
            return new DatosNegocioResponse
            {
                Codigo = 200,
                Mensaje = "OK",
                Datos = new DatosRespuesta { Rodillo = rodillo }
            };
        }

        public static DatosNegocioResponse Error(string mensaje, int codigo = 500)
        {
            return new DatosNegocioResponse
            {
                Codigo = codigo,
                Mensaje = mensaje,
                Datos = null
            };
        }
    }
}
