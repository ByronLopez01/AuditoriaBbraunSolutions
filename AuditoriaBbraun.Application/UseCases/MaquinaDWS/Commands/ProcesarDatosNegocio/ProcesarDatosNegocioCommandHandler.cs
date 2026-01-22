using MediatR;
using Microsoft.Extensions.Logging;

namespace AuditoriaBbraun.Application.UseCases.MaquinaDWS.Commands.ProcesarDatosNegocio
{
    public class ProcesarDatosNegocioCommandHandler : IRequestHandler<ProcesarDatosNegocioCommand, ProcesarDatosNegocioResponse>
    {
        private readonly ILogger<ProcesarDatosNegocioCommandHandler> _logger;

        public ProcesarDatosNegocioCommandHandler(ILogger<ProcesarDatosNegocioCommandHandler> logger)
        {
            _logger = logger;
        }

        public async Task<ProcesarDatosNegocioResponse> Handle(ProcesarDatosNegocioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Procesando datos DWS - Dispositivo: {Dispositivo}, Barcode: {Barcode}",
                    request.DeviceSn, request.Barcode);

                if (TieneMedidasNegativas(request))
                {
                    return ProcesarDatosNegocioResponse.Error(400, "Los valores de peso/dimensiones no pueden ser negativos");
                }

                var roller = DeterminarDireccionRodillo(request.Weight, request.Volume);

                await Task.CompletedTask;

                return ProcesarDatosNegocioResponse.Ok(roller);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ProcesarDatosNegocioCommandHandler");
                return ProcesarDatosNegocioResponse.Error(500, "Error interno al procesar datos");
            }
        }

        private static bool TieneMedidasNegativas(ProcesarDatosNegocioCommand request) =>
            (request.Weight.HasValue && request.Weight.Value < 0) ||
            (request.Length.HasValue && request.Length.Value < 0) ||
            (request.Width.HasValue && request.Width.Value < 0) ||
            (request.Height.HasValue && request.Height.Value < 0) ||
            (request.Volume.HasValue && request.Volume.Value < 0);

        private static string DeterminarDireccionRodillo(decimal? weight, decimal? volume)
        {
            var peso = weight ?? 0;
            var vol = volume ?? 0;

            if (peso > 50) return "exitport1";
            if (vol > 100) return "router2";
            if (peso > 20 && vol > 50) return "router1";

            return "router0";
        }
    }
}
