using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AuditoriaBbraun.Application.UseCases.MaquinaDWS.Commands.ProcesarDatosNegocio
{
    public class ProcesarDatosNegocioCommand : IRequest<ProcesarDatosNegocioResponse>
    {
        public string CodigoBarras { get; set; }
        public decimal Peso { get; set; }
        public decimal Largo { get; set; }
        public decimal Ancho { get; set; }
        public decimal Alto { get; set; }
        public decimal Volumen { get; set; }
        public string RutaImagen1 { get; set; }
        public string RutaImagen2 { get; set; }
        public string RutaImagen3 { get; set; }
        public string RutaImagenCompleta { get; set; }
        public string RutaCapturaPantalla { get; set; }
        public string FechaHora { get; set; }
        public string NumeroSerieDispositivo { get; set; }
    }
}
