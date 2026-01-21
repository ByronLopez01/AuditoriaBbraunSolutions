using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditoriaBbraun.Domain.Exceptions;

namespace AuditoriaBbraun.Domain.ValueObjects
{
    public class Dimensiones
    {
        public decimal Largo { get; }
        public decimal Ancho { get; }
        public decimal Alto { get; }
        public decimal Volumen { get; }

        public Dimensiones(decimal largo, decimal ancho, decimal alto)
        {
            if(largo<=0) 
                throw DimensionesInvalidasExcepcion.LargoInvalido(largo);

            if(ancho<=0) 
                throw DimensionesInvalidasExcepcion.AnchoInvalido(ancho);

            if(alto<=0) 
                throw DimensionesInvalidasExcepcion.AltoInvalido(alto);

            if (largo > 1000 || ancho > 1000 || alto > 1000)
                throw DimensionesInvalidasExcepcion.DimensionesFueraDeRango(largo, ancho, alto, 1000, 1000, 1000);

            Largo = largo;
            Ancho = ancho;
            Alto = alto;
            Volumen = Largo * Ancho * Alto;
        }

        private decimal CalcularVolumen(decimal largo, decimal ancho, decimal alto)
        {
            return Math.Round(largo * ancho * alto, 2);
        }

        public override bool Equals(object obj)
        {
            return obj is Dimensiones otras &&
                   Largo == otras.Largo &&
                   Ancho == otras.Ancho &&
                   Alto == otras.Alto;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Largo, Ancho, Alto);
        }
    }
}
