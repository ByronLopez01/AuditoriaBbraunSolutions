using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AuditoriaBbraun.Application.UseCases.MaquinaDWS.Commands.ProcesarDatosNegocio
{
    public class ProcesarDatosNegocioCommandValidator : AbstractValidator<ProcesarDatosNegocioCommand>
    {
        public ProcesarDatosNegocioCommandValidator()
        {
            RuleFor(x => x.CodigoBarras)
                .NotEmpty().WithMessage("El código de barras es requerido")
                .MaximumLength(500).WithMessage("El código de barras no puede exceder 500 caracteres");

            RuleFor(x => x.Peso)
                .GreaterThan(0).WithMessage("El peso debe ser mayor a cero");

            RuleFor(x => x.Largo)
                .GreaterThan(0).WithMessage("El largo debe ser mayor a cero");

            RuleFor(x => x.Ancho)
                .GreaterThan(0).WithMessage("El ancho debe ser mayor a cero");

            RuleFor(x => x.Alto)
                .GreaterThan(0).WithMessage("El alto debe ser mayor a cero");

            RuleFor(x => x.Volumen)
                .GreaterThan(0).WithMessage("El volumen debe ser mayor a cero");

            RuleFor(x => x.NumeroSerieDispositivo)
                .NotEmpty().WithMessage("El número de serie del dispositivo es requerido")
                .MaximumLength(100).WithMessage("El número de serie no puede exceder 100 caracteres");

            RuleFor(x => x.FechaHora)
                .NotEmpty().WithMessage("La fecha y hora son requeridas")
                .Matches(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$")
                .WithMessage("Formato de fecha inválido. Use: yyyy-MM-dd HH:mm:ss");
        }
    }
}
