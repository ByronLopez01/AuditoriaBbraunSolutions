using FluentValidation;

namespace AuditoriaBbraun.Application.UseCases.MaquinaDWS.Commands.ProcesarDatosNegocio
{
    public class ProcesarDatosNegocioCommandValidator : AbstractValidator<ProcesarDatosNegocioCommand>
    {
        public ProcesarDatosNegocioCommandValidator()
        {
            RuleFor(x => x.Barcode)
                .MaximumLength(500).WithMessage("El código de barras no puede exceder 500 caracteres");

            RuleFor(x => x.Weight)
                .GreaterThanOrEqualTo(0).When(x => x.Weight.HasValue)
                .WithMessage("El peso no puede ser negativo");

            RuleFor(x => x.Length)
                .GreaterThanOrEqualTo(0).When(x => x.Length.HasValue)
                .WithMessage("El largo no puede ser negativo");

            RuleFor(x => x.Width)
                .GreaterThanOrEqualTo(0).When(x => x.Width.HasValue)
                .WithMessage("El ancho no puede ser negativo");

            RuleFor(x => x.Height)
                .GreaterThanOrEqualTo(0).When(x => x.Height.HasValue)
                .WithMessage("El alto no puede ser negativo");

            RuleFor(x => x.Volume)
                .GreaterThanOrEqualTo(0).When(x => x.Volume.HasValue)
                .WithMessage("El volumen no puede ser negativo");

            RuleFor(x => x.DeviceSn)
                .MaximumLength(100).WithMessage("El número de serie no puede exceder 100 caracteres");

            RuleFor(x => x.Timestamp)
                .Matches(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$")
                .When(x => !string.IsNullOrWhiteSpace(x.Timestamp))
                .WithMessage("Formato de fecha inválido. Use: yyyy-MM-dd HH:mm:ss");
        }
    }
}
