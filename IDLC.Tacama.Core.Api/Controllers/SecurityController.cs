using IDCL.AVGUST.SIP.Manager.Tacama;
using IDLC.Tacama.Core.Api.Models;
using Microsoft.AspNetCore.Mvc;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers;

namespace IDLC.Tacama.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ILogger<SecurityController> _logger;
        private readonly ITacamaManager _tacamaManager;

        public SecurityController(ILogger<SecurityController> logger, ITacamaManager tacamaManager)
        {
            _logger = logger;
            _tacamaManager = tacamaManager;
        }

        [HttpGet("login/{usuario}/{clave}")]
        public async Task<IActionResult> login(string usuario, string clave)
        {

            var resp = new response();

            try
            {
                var response = await _tacamaManager.login(usuario, clave);
                if (response.EsError)
                {
                    resp.status = "Error";
                    resp.message = "Validaciones";

                    ModelState.AddModelError("validacion", response.MensajeError);
                    resp.data = ExtensionTools.Validaciones(ModelState);
                    return UnprocessableEntity(resp);
                }
                resp.status = "OK";
                resp.message = "OK";
                resp.data = response;
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
