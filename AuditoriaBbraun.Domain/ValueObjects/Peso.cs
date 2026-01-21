using AuditoriaBbraun.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.ValueObjects
{
    public class Peso
    {
        public decimal Valor { get; }
        public string Unidad { get; } = "kg";

        public Peso(decimal valor)
        {
            if (valor <= 0)
                throw PesoInvalidoExcepcion.PesoCero();

            if(valor<0)
                throw PesoInvalidoExcepcion.PesoNegativo(valor);

            //Ajustar peso maximo 

            if (valor > 1000)
                throw PesoInvalidoExcepcion.ExcedePesoMaximo(valor, 1000);


            Valor = valor;
        }

        public override bool Equals(object obj)
        {
            return obj is Peso otroPeso &&Valor == otroPeso.Valor && Unidad == otroPeso.Unidad;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Valor, Unidad);
        }
    }
}
