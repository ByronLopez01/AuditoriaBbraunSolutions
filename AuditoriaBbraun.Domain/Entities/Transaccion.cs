using AuditoriaBbraun.Domain.Exceptions;
using AuditoriaBbraun.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Entities
{
    public class Transaccion : IEntidadAgregado
    {
        public int Id { get; private set; }
        public string CodigoBarras { get; private set; }
        public decimal Peso { get; private set; } // en kg
        public decimal Largo { get; private set; } // en cm
        public decimal Ancho { get; private set; } // en cm
        public decimal Alto { get; private set; } // en cm
        public decimal Volumen { get; private set; } // en cm3
        public string NumeroSerieDispositivo { get; private set; }
        public DateTime FechaHora { get; private set; }
        public string RutaImagen1 { get; private set; }
        public string RutaImagen2 { get; private set; }
        public string RutaImagen3 { get; private set; }
        public string RutaImagenCompleta { get; private set; }
        public string RutaCapturaPantalla { get; private set; }
        public DateTime FechaCreacion { get; private set; }

       
        private Transaccion() { }

        public static Transaccion Crear(string codigoBarras,decimal peso,decimal largo,decimal ancho,decimal alto,string numeroSerieDispositivo,string rutaImagen1 = null,string rutaImagen2 = null,string rutaImagen3 = null,string rutaImagenCompleta = null,string rutaCapturaPantalla = null)
        {
            if (string.IsNullOrWhiteSpace(codigoBarras))
                throw new ArgumentException("El código de barras es requerido", nameof(codigoBarras));

            if (peso <= 0)
                throw new PesoInvalidoExcepcion("El peso debe ser mayor a cero");

            if (string.IsNullOrEmpty(codigoBarras))
                throw new ArgumentException("El código de barras es requerido");

            return new Transaccion
            {
                CodigoBarras = codigoBarras,
                Peso = peso,
                Largo = largo,
                Ancho = ancho,
                Alto = alto,
                Volumen = CalcularVolumen(largo, ancho, alto),
                NumeroSerieDispositivo = numeroSerieDispositivo,
                RutaImagen1 = rutaImagen1,
                RutaImagen2 = rutaImagen2,
                RutaImagen3 = rutaImagen3,
                RutaImagenCompleta = rutaImagenCompleta,
                RutaCapturaPantalla = rutaCapturaPantalla,
                FechaHora = DateTime.UtcNow,
                FechaCreacion = DateTime.UtcNow
            };
        }

        private static decimal CalcularVolumen(decimal largo, decimal ancho, decimal alto)
        {
            return Math.Round(largo * ancho * alto, 2);
        }

        public void ActualizarRutasImagenes(string rutaImagen1 = null,string rutaImagen2 = null,string rutaImagen3 = null,string rutaImagenCompleta = null,string rutaCapturaPantalla = null)
        {
            if (!string.IsNullOrWhiteSpace(rutaImagen1))
                RutaImagen1 = rutaImagen1;

            if (!string.IsNullOrWhiteSpace(rutaImagen2))
                RutaImagen2 = rutaImagen2;

            if (!string.IsNullOrWhiteSpace(rutaImagen3))
                RutaImagen3 = rutaImagen3;

            if (!string.IsNullOrWhiteSpace(rutaImagenCompleta))
                RutaImagenCompleta = rutaImagenCompleta;

            if (!string.IsNullOrWhiteSpace(rutaCapturaPantalla))
                RutaCapturaPantalla = rutaCapturaPantalla;
        }
    }
}
