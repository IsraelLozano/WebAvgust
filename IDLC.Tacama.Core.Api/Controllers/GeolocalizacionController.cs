using IDCL.AVGUST.SIP.Entity.Pedido;
using IDCL.AVGUST.SIP.Manager.Tacama;
using IDCL.AVGUST.SIP.ManagerDto.Tacama.TramaDiario;
using IDLC.Tacama.Core.Api.Models;
using Microsoft.AspNetCore.Mvc;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers;
using System.Net;

namespace IDLC.Tacama.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeolocalizacionController : ControllerBase
    {
        private readonly ILogger<GeolocalizacionController> _logger;
        private readonly ITacamaManager _tacamaManager;

        public GeolocalizacionController(ILogger<GeolocalizacionController> logger, ITacamaManager tramaDiarioRepository)
        {
            _logger = logger;
            _tacamaManager = tramaDiarioRepository;
        }

        [HttpGet("GetGeoByIdPersona/{IdPersona:int}")]
        public async Task<IActionResult> GetGeoByIdPersona(int IdPersona)
        {
            var resp = new response
            {
                status = "OK",
                message = "OK",
                data = await _tacamaManager.GetTramaListByIdPersona(IdPersona)
            };

            return Ok(resp);
        }


        [HttpPost("AddTrama")]
        public async Task<IActionResult> AddTrama(GetTramaDiarioDto model)
        {
            var resp = new response();

            try
            {
                if (model.Latitud == 0) ModelState.AddModelError("Latitud", "Latitud no valido");
                if (model.Longitud == 0) ModelState.AddModelError("Longitud", "Longitud no valido");
                if (!ModelState.IsValid)
                {
                    resp.status = "Error";
                    resp.message = "Validaciones";
                    resp.data = ExtensionTools.Validaciones(ModelState);

                    return UnprocessableEntity(resp);
                }

                resp.status = "OK";
                resp.message = "OK";
                resp.data = await _tacamaManager.AddTramaDiaria(model);
                //var response = await _tacamaManager.AddTramaDiaria(model);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("validacion", ex.Message);
                UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
