using AuditoriaBbraun.API.Models.Request;
using AuditoriaBbraun.API.Models.Response;
using AuditoriaBbraun.Application.UseCases.MaquinaDWS.Commands.ProcesarDatosNegocio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuditoriaBbraun.API.Controllers
{
    [Route("api/Dispositivos")]
    [ApiController]
    public class DispositivosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DispositivosController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DispositivosController(IMediator mediator, ILogger<DispositivosController> logger, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _logger = logger;
            _environment = environment;
        }

        /// <summary>
        /// POST /api/dispositivos/procesar-datos
        /// Recibe JSON de negocio (barcode, weight, dimensions, images, timestamp, devicesn).
        /// </summary>
        [HttpPost("procesar-datos")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> ProcesarDatosNegocio([FromBody] DatosNegocioRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errores = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Ok(ProcesarDatosNegocioResponse.Error(400, errores));
            }

            var command = new ProcesarDatosNegocioCommand
            {
                Barcode = request.Barcode,
                Weight = request.Weight,
                Length = request.Length,
                Width = request.Width,
                Height = request.Height,
                Volume = request.Volume,
                Image1Path = request.Image1Path,
                Image2Path = request.Image2Path,
                Image3Path = request.Image3Path,
                ImageAllPath = request.ImageAllPath,
                ScreenshotPath = request.ScreenshotPath,
                Image1Data = request.Image1Data,
                Image2Data = request.Image2Data,
                Image3Data = request.Image3Data,
                ImageAllData = request.ImageAllData,
                ScreenshotData = request.ScreenshotData,
                Timestamp = request.Timestamp,
                DeviceSn = request.DeviceSn
            };

            var resultado = await _mediator.Send(command, HttpContext.RequestAborted);
            
            // TODO!!! AGREGAR IMPLEMENTACIÓN REAL
            return Ok(resultado);
        }

        /// <summary>
        /// POST /api/Dispositivos/subir-imagen
        /// multipart/form-data: barcode, type (code|depth|env|screen|label), image.
        /// </summary>
        [HttpPost("subir-imagen")]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        public async Task<IActionResult> SubirImagen([FromForm] SubirImagenRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errores = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Ok(ApiResponse<object>.Fail(400, errores));
            }

            if (request.Imagen == null || request.Imagen.Length == 0)
            {
                return Ok(ApiResponse<object>.Fail(400, "La imagen es requerida"));
            }

            const long maxFileSizeBytes = 10 * 1024 * 1024; // 10 MB MAX
            if (request.Imagen.Length > maxFileSizeBytes)
            {
                return Ok(ApiResponse<object>.Fail(413, "La imagen supera el tamaño máximo permitido (10 MB)"));
            }

            var uploadsPath = Path.Combine(_environment.ContentRootPath, "wwwroot", "uploads", "imagenes");
            Directory.CreateDirectory(uploadsPath);

            var sanitizedName = Path.GetFileName(request.Imagen.FileName);
            var fileName = $"{request.CodigoBarras}_{request.Tipo}_{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}{Path.GetExtension(sanitizedName)}";
            var filePath = Path.Combine(uploadsPath, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Imagen.CopyToAsync(stream, HttpContext.RequestAborted);
            }

            _logger.LogInformation("Imagen guardada en {Ruta} (barcode: {Barcode}, tipo: {Tipo})",
                filePath, request.CodigoBarras, request.Tipo);

            // TODO!!! AGREGAR IMPLEMENTACIÓN REAL
            return Ok(ApiResponse<object>.Success(msg: "OK"));
        }

        /// <summary>
        /// POST /api/Dispositivos/controlar-leds
        /// JSON: { "slotStatus": { "slotNo": 1, "status": "free|work|done" } }
        /// </summary>
        [HttpPost("controlar-leds")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult ControlarLEDs([FromBody] ControlarLEDsRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errores = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Ok(ApiResponse<object>.Fail(400, errores));
            }

            var estado = request.EstadoSlot!;
            _logger.LogInformation("Controlar LEDs - Slot: {Slot}, Estado: {Estado}", estado.NumeroSlot, estado.Estado);

            // TODO!!! AGREGAR IMPLEMENTACIÓN REAL
            return Ok(ApiResponse<object>.Success(msg: "OK"));
        }

        /// <summary>
        /// GET /api/Dispositivos/salud
        /// </summary>
        [HttpGet("salud")]
        public IActionResult VerificarSalud()
        {
            // TODO!!! AGREGAR IMPLEMENTACIÓN REAL
            return Ok(new
            {
                Estado = "OK",
                Mensaje = "API de dispositivos DWSSystem funcionando",
                Fecha = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
            });
        }
    }
}
