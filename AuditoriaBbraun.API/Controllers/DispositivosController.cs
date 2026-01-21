using AuditoriaBbraun.API.Models.Request;
using AuditoriaBbraun.Application.UseCases.MaquinaDWS.Commands.ProcesarDatosNegocio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuditoriaBbraun.API.Controllers
{
    [Route("api/dispositivos")]
    [ApiController]
    public class DispositivosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DispositivosController> _logger;

        public DispositivosController(IMediator mediator, ILogger<DispositivosController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint principal para recibir datos de la máquina DWSSystem
        /// POST /api/dispositivos/procesar-datos
        /// </summary>
        [HttpPost("procesar-datos")]
        [Consumes("application/json")]
        public async Task<IActionResult> ProcesarDatosNegocio([FromBody] DatosNegocioRequest request)
        {
            try
            {
                _logger.LogInformation("Recibiendo datos de máquina - Dispositivo: {Dispositivo}",
                    request.NumeroSerieDispositivo);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var command = new ProcesarDatosNegocioCommand
                {
                    CodigoBarras = request.CodigoBarras,
                    Peso = request.Peso,
                    Largo = request.Largo,
                    Ancho = request.Ancho,
                    Alto = request.Alto,
                    Volumen = request.Volumen,
                    NumeroSerieDispositivo = request.NumeroSerieDispositivo,
                    RutaImagen1 = request.RutaImagen1,
                    RutaImagen2 = request.RutaImagen2,
                    RutaImagen3 = request.RutaImagen3,
                    RutaImagenCompleta = request.RutaImagenCompleta,
                    RutaCapturaPantalla = request.RutaCapturaPantalla,
                    FechaHora = request.FechaHora
                };

                var resultado = await _mediator.Send(command);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en endpoint ProcesarDatosNegocio");
                return StatusCode(500, new { Error = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Endpoint para subir imágenes desde la máquina
        /// POST /api/dispositivos/subir-imagen
        /// </summary>
        [HttpPost("subir-imagen")]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        public async Task<IActionResult> SubirImagen([FromForm] SubirImagenRequest request)
        {
            try
            {
                _logger.LogInformation("Subiendo imagen - Barcode: {Barcode}, Tipo: {Tipo}",
                    request.CodigoBarras, request.Tipo);

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Codigo = 400, Mensaje = "Datos de imagen inválidos" });
                }

                // Validar tipo de imagen
                var tiposPermitidos = new[] { "code", "depth", "env", "screen", "label" };
                if (!tiposPermitidos.Contains(request.Tipo))
                {
                    return BadRequest(new
                    {
                        Codigo = 400,
                        Mensaje = $"Tipo de imagen inválido. Tipos permitidos: {string.Join(", ", tiposPermitidos)}"
                    });
                }

                // Guardar imagen
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "imagenes");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                var fileName = $"{request.CodigoBarras}_{request.Tipo}_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}{Path.GetExtension(request.Imagen.FileName)}";
                var filePath = Path.Combine(uploadsPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Imagen.CopyToAsync(stream);
                }

                _logger.LogInformation("Imagen guardada: {FilePath}", filePath);

                return Ok(new
                {
                    Codigo = 200,
                    Mensaje = "Imagen subida correctamente",
                    RutaArchivo = filePath
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al subir imagen");
                return StatusCode(500, new
                {
                    Codigo = 500,
                    Mensaje = "Error interno al subir imagen"
                });
            }
        }

        /// <summary>
        /// Endpoint para controlar LEDs (solo dispositivos tipo Z)
        /// POST /api/dispositivos/controlar-leds
        /// </summary>
        [HttpPost("controlar-leds")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> ControlarLEDs([FromBody] ControlarLEDsRequest request)
        {
            try
            {
                _logger.LogInformation("Controlar LEDs - Slot: {Slot}",
                    request.EstadoSlot?.NumeroSlot);

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { Codigo = 400, Mensaje = "Datos de control inválidos" });
                }

                // Validar estado
                var estadosPermitidos = new[] { "free", "work", "done" };
                if (!estadosPermitidos.Contains(request.EstadoSlot.Estado))
                {
                    return BadRequest(new { Codigo = 400, Mensaje = "Estado de LED inválido" });
                }

                // Aquí iría la lógica real para controlar LEDs
                _logger.LogInformation("LED controlado - Slot: {Slot}, Estado: {Estado}",
                    request.EstadoSlot.NumeroSlot, request.EstadoSlot.Estado);

                return Ok(new
                {
                    Codigo = 200,
                    Mensaje = "LEDs controlados correctamente"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al controlar LEDs");
                return StatusCode(500, new
                {
                    Codigo = 500,
                    Mensaje = "Error interno al controlar LEDs"
                });
            }
        }

        /// <summary>
        /// Endpoint de salud
        /// GET /api/dispositivos/salud
        /// </summary>
        [HttpGet("salud")]
        public IActionResult VerificarSalud()
        {
            return Ok(new
            {
                Estado = "OK",
                Mensaje = "API de dispositivos DWSSystem funcionando",
                Fecha = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            });
        }
    }
}
