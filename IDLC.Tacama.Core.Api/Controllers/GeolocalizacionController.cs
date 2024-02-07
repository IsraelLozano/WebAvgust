using IDCL.AVGUST.SIP.Manager.Tacama;
using IDCL.AVGUST.SIP.ManagerDto.Tacama.TramaDiario;
using Microsoft.AspNetCore.Mvc;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers;

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
        public async Task<IActionResult> GetGeoByIdPersona(int IdPersona) => Ok(await _tacamaManager.GetTramaListByIdPersona(IdPersona));

        [HttpPost("AddTrama")]
        public async Task<IActionResult> AddTrama(GetTramaDiarioDto model)
        {
            try
            {

                if (model.Latitud == 0) ModelState.AddModelError("Latitud", "Latitud no valido");

                if (model.Longitud == 0) ModelState.AddModelError("Longitud", "Longitud no valido");

                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ExtensionTools.Validaciones(ModelState));
                }
                var response = await _tacamaManager.AddTramaDiaria(model);
                return Ok(response);
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
