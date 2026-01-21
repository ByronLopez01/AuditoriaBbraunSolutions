using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditoriaBbraun.Domain.Interfaces;
using AuditoriaBbraun.Domain.Services;
using Microsoft.Extensions.Logging;
using AuditoriaBbraun.Domain.Entities;

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
                _logger.LogInformation("Procesando datos de negocio - Dispositivo: {Dispositivo}",
                    request.NumeroSerieDispositivo);

                // Validaciones básicas
                if (request.Peso <= 0 || request.Largo <= 0 || request.Ancho <= 0 || request.Alto <= 0)
                {
                    return new ProcesarDatosNegocioResponse
                    {
                        Codigo = 400,
                        Mensaje = "Datos de peso/dimensiones inválidos",
                        Datos = null
                    };
                }

                
                string direccionRodillo = DeterminarDireccionRodillo(request.Peso, request.Volumen);

                
                await Task.Delay(10, cancellationToken);

               
                return new ProcesarDatosNegocioResponse
                {
                    Codigo = 200,
                    Mensaje = "OK",
                    Datos = new ProcesarDatosNegocioResponse.DatosRespuesta
                    {
                        Rodillo = direccionRodillo
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ProcesarDatosNegocioCommandHandler");
                return new ProcesarDatosNegocioResponse
                {
                    Codigo = 500,
                    Mensaje = "Error interno al procesar datos",
                    Datos = null
                };
            }
        }

            private string DeterminarDireccionRodillo(decimal peso, decimal volumen)
        {

            if (peso > 50) return "exitport1";
            if (volumen > 100) return "router2";
            if (peso > 20 && volumen > 50) return "router1";

            return "router0";
        }
    }
}
