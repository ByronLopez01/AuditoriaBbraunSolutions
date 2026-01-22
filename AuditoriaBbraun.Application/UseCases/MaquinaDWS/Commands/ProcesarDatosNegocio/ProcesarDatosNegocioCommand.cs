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
        public string? Barcode { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? Volume { get; set; }
        public string? Image1Path { get; set; }
        public string? Image2Path { get; set; }
        public string? Image3Path { get; set; }
        public string? ImageAllPath { get; set; }
        public string? ScreenshotPath { get; set; }
        public string? Image1Data { get; set; }
        public string? Image2Data { get; set; }
        public string? Image3Data { get; set; }
        public string? ImageAllData { get; set; }
        public string? ScreenshotData { get; set; }
        public string? Timestamp { get; set; }
        public string? DeviceSn { get; set; }
    }
}
