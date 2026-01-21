using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Application.UseCases.MaquinaDWS.Commands.ProcesarDatosNegocio
{
    public class ProcesarDatosNegocioResponse
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
        public DatosRespuesta Datos { get; set; }

        public class DatosRespuesta
        {
            public string Rodillo { get; set; }
        }
    }
}
